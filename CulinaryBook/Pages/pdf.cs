using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Font;
using iText.Kernel.Colors;
using iText.IO.Image;
using Paragraph = iText.Layout.Element.Paragraph;
using Image = iText.Layout.Element.Image;
using TextAlignment = iText.Layout.Properties.TextAlignment;
using Document = iText.Layout.Document;

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
