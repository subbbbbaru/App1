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
            Stream stream_Check_Pack = assembly.GetManifestResourceStream("App1.MyResources.CheckXML.xml");
            var serialize_Check_Pack = new XmlSerializer(typeof(CheckPackage));
            CheckPackage parameters_Check_Pack = (CheckPackage)serialize_Check_Pack.Deserialize(stream_Check_Pack);

            Stream stream_Doc_Out_Param = assembly.GetManifestResourceStream("App1.MyResources.DocumentOutput.xml");
            var serialize_Doc_Out_Param = new XmlSerializer(typeof(DocumentOutputParameters));
            DocumentOutputParameters parameters_Doc_Out_Param = (DocumentOutputParameters)serialize_Doc_Out_Param.Deserialize(stream_Doc_Out_Param);


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




            string text1 = $"КАССОВЫЙ ЧЕК №{parameters_Doc_Out_Param.Parameters.CheckNumber}";
            Text text = new Text(text1);

            text.SetFont(pdfFont);
            text.SetFontSize(14);
            Paragraph p_Title_check_Number = new Paragraph();

            iText.Kernel.Colors.Color color = new DeviceRgb(255, 150, 20);
            //text.SetFontColor(color);
            p_Title_check_Number.SetFont(pdfFont);
            
            //разделитель
            LineSeparator lineSeparator = new LineSeparator(new SolidLine());
            document.Add(lineSeparator);

            p_Title_check_Number.Add(text).SetTextAlignment(TextAlignment.CENTER);
            document.Add(p_Title_check_Number);

            document.Add(lineSeparator);

            Paragraph p_check_product = new Paragraph();
            p_check_product.SetFont(pdfFont);

            foreach (var fiscalString in parameters_Check_Pack.Positions.FiscalString)
            {
                Text te = new Text(fiscalString.Name);
                te.SetFont(pdfFont);
                te.SetFontSize(14);
                te.SetTextAlignment(TextAlignment.LEFT);
                Text te2 = new Text(fiscalString.PriceWithDiscount);
                te2.SetFont(pdfFont);
                te2.SetFontSize(14);
                te2.SetTextAlignment(TextAlignment.CENTER);

                p_check_product.Add(te/*fiscalString.Name + "\t" + fiscalString.PriceWithDiscount*/);
                p_check_product.Add(new Tab());
                p_check_product.Add(te2);
                p_check_product.Add("\n");
                
            }
            document.Add(p_check_product);
            //p_check_product.SetTextAlignment(TextAlignment.LEFT);



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
            return  imgBar;
        }
        //private void AddTitle(Document document, string text, int align, )
        //{

        //}


    }
}
