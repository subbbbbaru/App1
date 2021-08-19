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
            PageSize pageSize = new PageSize(PageSize.B7);
            Document document = new Document(pdfDoc, pageSize);
            PdfFont pdfFont = PdfFontFactory.CreateFont(FontConstants.TIMES_ROMAN, "CP1250", true);

            

            Text text = new Text("мамам");
            text.SetFont(pdfFont);
            text.SetFontSize(14);
            Paragraph paragraph1 = new Paragraph();

            Color color = new DeviceRgb(255, 100, 20);
            text.SetFontColor(color);

            //foreach (var fiscalString in parameters.Positions.FiscalString)
            //{
            //    paragraph1.Add(fiscalString.Name); 
            //}



            paragraph1.Add(text);
            document.Add(paragraph1);

            

            //разделитель
            LineSeparator lineSeparator = new LineSeparator(new SolidLine());
            document.Add(lineSeparator);


            //добавить QR-код в приложение
            document.Add(CreateQRcode(pdfDoc));


            //сохранение документа
            document.Close();

        }


        Image CreateQRcode(PdfDocument pdfDoc, string time = "1", string sum = "1", string fn = "1", string i = "1", string fp = "1", string n = "1")
        {
            //создание QR-кода
            BarcodeQRCode barcodeQRCode = new BarcodeQRCode(time + sum + fn + i + fp + n);
            //конвертация QR-кода в изображение
            var imgBar = new Image(barcodeQRCode.CreateFormXObject(pdfDoc));
            imgBar.SetWidth(56.6929f);
            imgBar.SetHeight(56.6929f);

            return  imgBar;
        }


    }
}
