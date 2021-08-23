using System;
using System.Collections.Generic;
using System.Text;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Barcodes;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Kernel.Geom;
using iText.Kernel.Font;
using iText.IO.Font;
using iText.Kernel.Colors;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;
using App1.Models;

namespace App1
{

    public class GeneratePDF
    {
        public void CreatePDFfile(PdfDocument pdfDoc)
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("App1.MyResources.CheckXML.xml");
            var serialize = new XmlSerializer(typeof(CheckPackage));
            CheckPackage parameters = (CheckPackage)serialize.Deserialize(stream);

            

            // var fisk = parameters.Positions.FiscalString.;

            //размер страницы PDF документа
            PageSize pageSize = new PageSize(PageSize.A4);
            Document document = new Document(pdfDoc, pageSize);

           

            Stream stream_font = assembly.GetManifestResourceStream("App1.MyResources.Serif-Thin.ttf");


            var memoryStream = new MemoryStream();
            stream_font.CopyTo(memoryStream);
            byte[] font_Serif = memoryStream.ToArray();

            // https://coderoad.ru/1080442/Как-преобразовать-поток-в-byte-в-C
            // https://docs.microsoft.com/ru-ru/xamarin/xamarin-forms/data-cloud/data/files?tabs=macos


            // PdfFont pdfFont = PdfFontFactory.CreateFont(font_Serif, "Cp1251", PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED)       

            PdfFont pdfFont = PdfFontFactory.CreateFont(font_Serif, PdfEncodings.IDENTITY_H, PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);




            string text1 = "Misha is Vine magnat!";
            Text text = new Text(text1);

            text.SetFont(pdfFont);
            text.SetFontSize(14);
            Paragraph paragraph1 = new Paragraph();

            iText.Kernel.Colors.Color color = new DeviceRgb(255, 150, 20);
            //text.SetFontColor(color);
            paragraph1.SetFont(pdfFont);
            //разделитель
            LineSeparator lineSeparator = new LineSeparator(new SolidLine());
            document.Add(lineSeparator);

            foreach (var fiscalString in parameters.Positions.FiscalString)
            {
                paragraph1.Add(fiscalString.Name + "________" + fiscalString.PriceWithDiscount);
                paragraph1.Add("\n");
            }

            paragraph1.Add(text);

            document.SetTextAlignment(TextAlignment.CENTER).Add(paragraph1);


            //добавить QR-код в приложение и установить местоположение
            Paragraph paragraph2 = new Paragraph();
            paragraph2.SetTextAlignment(TextAlignment.CENTER).Add(CreateQRcode(pdfDoc, text1));
            document.Add(paragraph2);


            //сохранение документа
            document.Close();

        }


        iText.Layout.Element.Image CreateQRcode(PdfDocument pdfDoc, string text)
        {
            //создание QR-кода
            BarcodeQRCode barcodeQRCode = new BarcodeQRCode(text);
            //конвертация QR-кода в изображение
            var imgBar = new iText.Layout.Element.Image(barcodeQRCode.CreateFormXObject(pdfDoc));
            imgBar.SetWidth(56.6929f);
            imgBar.SetHeight(56.6929f);
            //imgBar.SetTextAlignment(TextAlignment.RIGHT);
            return  imgBar;
        }
        //private void AddTitle(Document document, string text, int align, )
        //{

        //}


    }
}
