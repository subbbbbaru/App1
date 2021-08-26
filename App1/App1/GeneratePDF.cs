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
                // Добавление товаров и их элементов в чек

                // help https://kb.itextpdf.com/home/it7kb/examples/splitting-tables
                // help https://kb.itextpdf.com/home/it7kb/ebooks/itext-7-building-blocks/chapter-6-creating-actions-destinations-and-bookmarks
                // help https://kb.itextpdf.com/home/it7kb/examples/itext-7-building-blocks-chapter-6-actions-destinations-bookmarks#iText7BuildingBlocks-Chapter6:actions,destinations,bookmarks-c06e04_toc_gotonamed

                
                

                // ячейка с именем товара
                Paragraph p_check_product_Name = new Paragraph(fiscalString.Name);
                Cell cell_Name = new Cell().Add(p_check_product_Name)
                    .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE);

                // ячейка с ценой товара
                Paragraph p_check_product_PriceWithDiscount = new Paragraph(fiscalString.PriceWithDiscount);
                Cell cell_PriceWithDiscount = new Cell().Add(p_check_product_PriceWithDiscount)
                    .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE);


                // ячейка с НДС товара
                Paragraph p_check_product_VATRate = new Paragraph($"НДС {fiscalString.VATRate}%");
                Cell cell_VATRate = new Cell().Add(p_check_product_VATRate)
                    .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetVerticalAlignment(VerticalAlignment.BOTTOM);

                // ячейка с Признаком способа расчета
                Paragraph p_check_product_PaymentMethod = new Paragraph(check_PaymentMethod(fiscalString.PaymentMethod));
                Cell cell_PaymentMethod = new Cell().Add(p_check_product_PaymentMethod)
                    .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE);
                
                // Добавление ячейки Имени и цены в таблицу
                t_check_product.AddCell(cell_Name);
                t_check_product.AddCell(cell_PriceWithDiscount);

                // Добавление ячейки НДС и Признаком способа расчета в таблицу
                t_check_product.AddCell(cell_VATRate);
                t_check_product.AddCell(cell_PaymentMethod);

            }
            // Добавить таблицу в документ
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

        // Системы налогообложения
        private string check_TaxationSystem(string TaxationSystem)
        {
            string taxSistem = TaxationSystem;
            string taxSistem_return = "";
            switch (taxSistem)
            {
                case "0":
                    taxSistem_return = "Общая";
                    break;
                case "1":
                    taxSistem_return = "Упрощенная (Доход)";
                    break;
                case "2":
                    taxSistem_return = "Упрощенная (Доход минус Расход)";
                    break;
                case "3":
                    taxSistem_return = "Единый налог на вмененный доход";
                    break;
                case "4":
                    taxSistem_return = "Единый сельскохозяйственный налог";
                    break;
                case "5":
                    taxSistem_return = "Патентная система налогообложения";
                    break;
                default:
                    taxSistem_return = "ERROR !!! Системы налогообложения!";
                    break;
            }
            return taxSistem_return;
        }

        // Признак способа расчета
        private string check_PaymentMethod(string PaymentMethod)
        {
            string priznak = PaymentMethod;
            string priznak_return = "";
            switch (priznak)
            {
                case "1":
                    priznak_return = "Предоплата полная";
                    break;
                case "2":
                    priznak_return = "Предоплата частичная";
                    break;
                case "3":
                    priznak_return = "Аванс";
                    break;
                case "4":
                    priznak_return = "Полный расчет";
                    break;
                case "5":
                    priznak_return = "Частичный расчет и кредит";
                    break;
                case "6":
                    priznak_return = "Передача в кредит";
                    break;
                case "7":
                    priznak_return = "Оплата кредита";
                    break;
                default:
                    priznak_return = "ERROR !!! Вызовите программиста!";
                    break;
            }
            return priznak_return;
        }
        // Признак предмета расчета
        private string check_CalculationSubject(string CalculationSubject)
        
        {
            string priznak = CalculationSubject;
            string priznak_return = "";
            switch (priznak)
            {
                case "1":
                    priznak_return = "Товар";
                    break;
                case "2":
                    priznak_return = "Подакцизный товар";
                    break;
                case "3":
                    priznak_return = "Работа";
                    break;
                case "4":
                    priznak_return = "Услуга";
                    break;
                case "5":
                    priznak_return = "Ставка азартной игры";
                    break;
                case "6":
                    priznak_return = "Выигрыш азартной игры";
                    break;
                case "7":
                    priznak_return = "Лотерейный билет";
                    break;
                case "8":
                    priznak_return = "Выигрыш лотереи";
                    break;
                case "9":
                    priznak_return = "Предоставление результатов интеллектуальной деятельности";
                    break;
                case "10":
                    priznak_return = "Платеж";
                    break;
                case "11":
                    priznak_return = "Агентское вознаграждение";
                    break;
                case "12":
                    priznak_return = "Выплата";
                    break;
                case "13":
                    priznak_return = "Иной предмет расчета";
                    break;
                case "14":
                    priznak_return = "Имущественное право";
                    break;
                case "15":
                    priznak_return = "Внереализационный доход";
                    break;
                case "16":
                    priznak_return = "Страховые взносы";
                    break;
                case "17":
                    priznak_return = "Торговый сбор";
                    break;
                case "18":
                    priznak_return = "Курортный сбор";
                    break;
                case "19":
                    priznak_return = "Залог";
                    break;
                case "20":
                    priznak_return = "Расход";
                    break;
                case "21":
                    priznak_return = "Взносы на обязательное пенсионное страхование ИП";
                    break;
                case "22":
                    priznak_return = "Взносы на обязательное пенсионное страхование";
                    break;
                case "23":
                    priznak_return = "Взносы на обязательное медицинское страхование ИП";
                    break;
                case "24":
                    priznak_return = "Взносы на обязательное медицинское страхование";
                    break;
                case "25":
                    priznak_return = "Взносы на обязательное социальное страхование";
                    break;
                case "26":
                    priznak_return = "Платеж казино";
                    break;
                default:
                    priznak_return = "ERROR !!! Признак агента по предмету расчета!";
                    break;
            }
            return priznak_return;
        }
        // Признак агента
        private string check_AgentType(string AgentType)
        {

            string priznak = AgentType;
            string priznak_return = "";
            switch (priznak)
            {
                case "0":
                    priznak_return = "Банковский платежный агент";
                    break;
                case "1":
                    priznak_return = "Банковский платежный субагент";
                    break;
                case "2":
                    priznak_return = "Платежный агент";
                    break;
                case "3":
                    priznak_return = "Платежный субагент";
                    break;
                case "4":
                    priznak_return = "Поверенный";
                    break;
                case "5":
                    priznak_return = "Комиссионер";
                    break;
                case "6":
                    priznak_return = "Агент";
                    break;
                default:
                    priznak_return = "ERROR !!! Признак агента!";
                    break;
            }
            return priznak_return;
        }
        // Признак агента по предмету расчета
        private string check_CalculationAgent(string CalculationAgent)
        {

            string priznak = CalculationAgent;
            string priznak_return = "";
            switch (priznak)
            {
                case "0":
                    priznak_return = "Банковский платежный агент";
                    break;
                case "1":
                    priznak_return = "Банковский платежный субагент";
                    break;
                case "2":
                    priznak_return = "Платежный агент";
                    break;
                case "3":
                    priznak_return = "Платежный субагент";
                    break;
                case "4":
                    priznak_return = "Поверенный";
                    break;
                case "5":
                    priznak_return = "Комиссионер";
                    break;
                case "6":
                    priznak_return = "Агент";
                    break;
                default:
                    priznak_return = "ERROR !!! Признак агента по предмету расчета!";
                    break;
            }
            return priznak_return;
        }

    }
}
