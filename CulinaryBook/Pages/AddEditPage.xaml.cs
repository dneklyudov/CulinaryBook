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
using Microsoft.Win32;
using iText.Layout.Element;
using System.IO;
using System.Diagnostics;

namespace CulinaryBook.Pages
{
    /// <summary>
    /// Interaction logic for AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {

        private Recipes _recipe = new Recipes();
        private string SelectedImage = "";

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

            _recipe.Image = SelectedImage;

            if (_recipe.RecipeID == 0)
            {
                AppConnect.model01.Recipes.Add(_recipe);
            }

            try
            {
                AppConnect.model01.SaveChanges();
                NavigationService.Navigate(new Pages.PageRecipes());
                // MessageBox.Show("Рецепт успешно добавлен", "Информация сохранена", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "При сохранении произошла ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnLoadPicture_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog();
                dialog.Filter = "Image Files:|*.jpg;*.png|All Files|*.*";
                dialog.Title = "Выберите изображение";

                string path = Directory.GetCurrentDirectory();
                string newPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(path, @"..\..\Images"));

                //Debug.WriteLine(Directory.GetCurrentDirectory() + @"\..\..\Images");
                //dialog.InitialDirectory =  + @"\..\..\Images";
                //Debug.WriteLine(Directory.GetCurrentDirectory());

                if (dialog.ShowDialog() == true) { 
                    SelectedImage = System.IO.Path.GetFileName(dialog.FileName);
                    txbImage.Text = SelectedImage;
                    // MessageBox.Show("Изображение успешно загружено.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Изображение не выбрано.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке изображения: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
