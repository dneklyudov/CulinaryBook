using CulinaryBook.ApplicationData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace CulinaryBook.Pages
{
    /// <summary>
    /// Interaction logic for AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {

        private Recipes _recipe = new Recipes();

        public AddEditPage(Recipes selectedRecipe)
        {
            InitializeComponent();

            if (selectedRecipe != null)
                _recipe = selectedRecipe;

            DataContext = _recipe;

            // cbCategory.ItemsSource = CulinaryBookEntities1.GetContext()
            // cbCategory.ItemsSource = AppConnect.model01.GetContext()

            cbCategory.ItemsSource = AppConnect.model01.Categories.ToList();
            cbAuthor.ItemsSource = AppConnect.model01.Authors.ToList();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            // ApplicationData.AppFrame.frmMain.GoBack();
            NavigationService.Navigate(new Pages.PageRecipes());
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            //RecipeName
            //Description
            //Category
            //Author
            //CookingTime
            //Image

            if (String.IsNullOrEmpty(_recipe.RecipeName) || String.IsNullOrWhiteSpace(_recipe.RecipeName))
            {
                errors.AppendLine("Заполните название");
            }

            if (String.IsNullOrEmpty(_recipe.Description) || String.IsNullOrWhiteSpace(_recipe.Description))
            {
                errors.AppendLine("Заполните описание");
            }

            if (_recipe.Categories == null)
            {
                errors.AppendLine("Выберите категорию");
            }

            if (_recipe.Authors == null)
            {
                errors.AppendLine("Выберите автора");
            }


            if (String.IsNullOrEmpty(_recipe.CookingTime.ToString()) || String.IsNullOrWhiteSpace(_recipe.CookingTime.ToString()))
            {
                errors.AppendLine("Заполните время приготовления");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Не заполнены обязательные поля", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _recipe.Image = "";

            if (_recipe.RecipeID == 0)
            {
                AppConnect.model01.Recipes.Add(_recipe);
            }

            try
            {
                AppConnect.model01.SaveChanges();
                NavigationService.Navigate(new Pages.PageRecipes());
                // MessageBox.Show("Рецепт успешно добавлен", "Информация сохранена", MessageBoxButton.OK, MessageBoxImage.Information);


                // В примере сказано: 

                // 7.Теперь можно вернуться назад.При возврате на страницу со списком отелей, нам необходимо
                // выводить актуальную информацию, обновляя список в таблице. Для этого мы будем использовать
                // событие у страницы IsVisibleChange.Оно срабатывает каждый раз, когда страница отображается,
                // либо скрывается

                // С помощью F12 переходим в код.Если видимость страницы isVisible, мы будем обращаться
                // к контексту с помощью свойства ChangeTracker ко всем сущностям, которые есть. И для каждой
                // из них будем выполнять метод перезагрузки и вывода актуальных данных.После этого таблицу
                // DGridHotels присвоим таблице «список отелей»

                // Зачем это все? И так работает за счет реактивности.

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "При сохранении произошла ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
