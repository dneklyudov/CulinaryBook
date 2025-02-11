using CulinaryBook.ApplicationData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Font;
using iText.Kernel.Colors;
using iText.IO.Image;
using Paragraph = iText.Layout.Element.Paragraph;
using Image = iText.Layout.Element.Image;
using TextAlignment = iText.Layout.Properties.TextAlignment;
using Document = iText.Layout.Document;

namespace CulinaryBook.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageRecipes.xaml
    /// </summary>
    public partial class PageRecipes : Page
    {
        public PageRecipes()
        {
            InitializeComponent();
            listProducts.ItemsSource = RecipesList(); // AppConnect.model01.Recipes.ToList();

            ComboSort.Items.Add("Сортировать по времени");
            ComboSort.Items.Add("По возрастанию");
            ComboSort.Items.Add("По убыванию");
            ComboSort.SelectedIndex = 0;

            var category = AppConnect.model01.Categories;
            ComboFilter.Items.Add("Категория");
            foreach (var item in category)
            {
                ComboFilter.Items.Add(item.CategoryName);
            }
            ComboFilter.SelectedIndex = 0;

        }

        Recipes[] RecipesList()
        {
            try
            {
                List<Recipes> recipes = AppConnect.model01.Recipes.ToList();
                
                if (TextSearch != null)
                {
                    recipes = recipes.Where(x => x.RecipeName.ToLower().Contains(TextSearch.Text.ToLower())).ToList();
                }

                if (ComboFilter.SelectedIndex > 0)
                {
                    // recipes = recipes.Where(x => x.CategoryID == ComboFilter.SelectedIndex).ToList(); // это неправильно, потому что они могут идти не подряд
                    switch (ComboFilter.SelectedIndex)
                    {
                        case 1:
                            recipes = recipes.Where(x => x.CategoryID == 1).ToList();
                            break;
                        case 2:
                            recipes = recipes.Where(x => x.CategoryID == 2).ToList();
                            break;
                        case 3:
                            recipes = recipes.Where(x => x.CategoryID == 3).ToList();
                            break;
                        case 4:
                            recipes = recipes.Where(x => x.CategoryID == 4).ToList();
                            break;
                    }
                }

                if (ComboSort.SelectedIndex > 0)
                {
                    switch (ComboSort.SelectedIndex)
                    {
                        case 1:
                            recipes = recipes.OrderBy(x => x.CookingTime).ToList();
                            break;
                        case 2:
                            recipes = recipes.OrderByDescending(x => x.CookingTime).ToList();
                            break;
                    }
                }

                if (recipes.Count > 0)
                {
                    tbCounter.Text = "Найдено " + recipes.Count + " шт.";
                }
                else
                {
                    tbCounter.Text = "Ничего не найдено";
                }

                return recipes.ToArray();
            }
            catch
            {
                MessageBox.Show("Повторите попытку");
                return null;
            }

        }

        private void TextSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            listProducts.ItemsSource = RecipesList();
        }

        private void ComboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listProducts.ItemsSource = RecipesList();
        }

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listProducts.ItemsSource = RecipesList();
        }

        private void BtnToPDF_Click(object sender, RoutedEventArgs e)
        {

            PdfDocument pdfDocument = new PdfDocument(
                new PdfWriter(
                    new FileStream("../../CulinaryBook.pdf", FileMode.Create, FileAccess.Write)
                )
            );
            Document document = new Document(pdfDocument);

            try
            {
                PdfFont font = PdfFontFactory.CreateFont("C:/Windows/Fonts/arial.ttf", "Identity-H");

                document.Add(new Paragraph()
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFont(font)
                    .SetFontSize(25)
                    .SetFontColor(ColorConstants.RED)
                    .Add("Список рецептов"));

                foreach (var item in AppConnect.model01.Recipes.ToList())
                {
                    if (item is Recipes data)
                    {
                        String imgURI = "../../Images/cook.jpg";
                        if (!(String.IsNullOrEmpty(item.Image) || String.IsNullOrWhiteSpace(item.Image))) imgURI = "../../Images/" + item.Image;

                        document.Add(new Image(ImageDataFactory.Create(imgURI))
                            .ScaleAbsolute(100, 100));

                        document.Add(new Paragraph()
                            .SetFont(font)
                            .Add("Название: " + data.RecipeName));
                        document.Add(new Paragraph()
                            .SetFont(font)
                            .Add("Описание: " + data.Description));
                        document.Add(new Paragraph()
                            .SetFont(font)
                            .Add("Время приготовления: " + data.CookingTime.ToString()));
                    }
                }
                
                MessageBox.Show("PDF-документ успешно создан.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex) {
                MessageBox.Show("При создании PDF-документа произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                document.Close();
            }

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.AddEditPage(null));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.AddEditPage((sender as Button).DataContext as Recipes));
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            var recipesForRemoving = listProducts.SelectedItems.Cast<Recipes>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить {recipesForRemoving.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    AppConnect.model01.Recipes.RemoveRange(recipesForRemoving);
                    AppConnect.model01.SaveChanges();
                    MessageBox.Show("Данные успешно удалены");

                    listProducts.ItemsSource = RecipesList();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
