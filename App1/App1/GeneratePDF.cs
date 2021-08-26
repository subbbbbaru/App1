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
using System.Collections;

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

            document.SetFont(pdfFont);


            string text1 = $"КАССОВЫЙ ЧЕК №{parameters_Doc_Out_Param.Parameters.CheckNumber}";
            Text text = new Text(text1);

            //text.SetFont(pdfFont);
            text.SetFontSize(14);
            Paragraph p_Title_check_Number = new Paragraph();

            iText.Kernel.Colors.Color color = new DeviceRgb(255, 150, 20);
            //text.SetFontColor(color);
            //p_Title_check_Number.SetFont(pdfFont);
            
            //разделитель
            LineSeparator lineSeparator = new LineSeparator(new SolidLine());
            document.Add(lineSeparator);

            p_Title_check_Number.Add(text).SetTextAlignment(TextAlignment.CENTER);
            document.Add(p_Title_check_Number);

            document.Add(lineSeparator);

            

            
            Table t_check_product = new Table(UnitValue.CreatePercentArray(new float[] { 7, 3})).UseAllAvailableWidth();

            foreach (var fiscalString in parameters_Check_Pack.Positions.FiscalString)
            {
                Paragraph p_check_product_Name = new Paragraph(fiscalString.Name);
                Paragraph p_check_product_PriceWithDiscount = new Paragraph(fiscalString.PriceWithDiscount);
                //Text text_Name = new Text("");
                Cell cell_Name = new Cell().Add(p_check_product_Name)
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE);
                // help https://kb.itextpdf.com/home/it7kb/examples/splitting-tables
                // help https://kb.itextpdf.com/home/it7kb/ebooks/itext-7-building-blocks/chapter-6-creating-actions-destinations-and-bookmarks
                // help https://kb.itextpdf.com/home/it7kb/examples/itext-7-building-blocks-chapter-6-actions-destinations-bookmarks#iText7BuildingBlocks-Chapter6:actions,destinations,bookmarks-c06e04_toc_gotonamed
                Cell cell_PriceWithDiscount = new Cell().Add(p_check_product_PriceWithDiscount)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE);

                t_check_product.AddCell(cell_Name);
                t_check_product.AddCell(cell_PriceWithDiscount);
                




            }
            document.Add(t_check_product);




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
        //public void check_Product(Table table, Paragraph par, Paragraph par2, string Name, string PriceWithDiscount)
        //{
        //    Text text_Name = new Text(Name + "\n");
        //    Cell cell_Name = new Cell().Add(par.Add(text_Name).SetTextAlignment(TextAlignment.LEFT));
        //    table.AddCell(cell_Name);


        //    Text text_PriceWithDiscount = new Text(PriceWithDiscount + "\n");
        //    Cell cell_PriceWithDiscount = new Cell().Add(par2.Add(text_PriceWithDiscount).SetTextAlignment(TextAlignment.RIGHT));
        //    table.AddCell(cell_PriceWithDiscount);
        //}


    }
}
