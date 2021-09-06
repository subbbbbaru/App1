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
using App1.Generate;
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


            // размер страницы PDF документа
            PageSize pageSize = new PageSize(PageSize.A4);
            Document document = new Document(pdfDoc, pageSize);

            // подключение шрифтов

            Stream stream_font = assembly.GetManifestResourceStream("App1.MyResources.Serif-Thin.ttf");


            var memoryStream = new MemoryStream();
            stream_font.CopyTo(memoryStream);
            byte[] font_Serif = memoryStream.ToArray();

            // https://coderoad.ru/1080442/Как-преобразовать-поток-в-byte-в-C
            // https://docs.microsoft.com/ru-ru/xamarin/xamarin-forms/data-cloud/data/files?tabs=macos
      

            PdfFont pdfFont = PdfFontFactory.CreateFont(font_Serif, PdfEncodings.IDENTITY_H, PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);

            document.SetFont(pdfFont); // установить шрифт


            string text1 = $"КАССОВЫЙ ЧЕК №{parameters_Doc_Out_Param.Parameters.CheckNumber}";
            Text text = new Text(text1);

            text.SetFontSize(14);
            Paragraph p_Title_check_Number = new Paragraph();

            
            // разделитель
            LineSeparator lineSeparator = new LineSeparator(new SolidLine());
            document.Add(lineSeparator);

            p_Title_check_Number.Add(text).SetTextAlignment(TextAlignment.CENTER);
            document.Add(p_Title_check_Number);

            document.Add(lineSeparator);

            // Генерация блока ИНН...КТТ...
            Gen_INN_KKT gen_INN_KKT = new Gen_INN_KKT();
            gen_INN_KKT.Gen_INN_kkt(document, parameters_Check_Pack, parameters_Doc_Out_Param);


            document.Add(lineSeparator);

            // Генерация блока Подуктов
            Gen_Check_Products gen_Check_Products = new Gen_Check_Products();
            gen_Check_Products.Gen_Check_Product(document, parameters_Check_Pack, parameters_Doc_Out_Param, lineSeparator);

            document.Add(lineSeparator);

            // Генерация блока QR code и нижней части чека
            Gen_QRcode gen_QRcode = new Gen_QRcode();
            gen_QRcode.Footer_Check(document: document, pdfDoc: pdfDoc, parameters_Check_Pack: parameters_Check_Pack, parameters_Doc_Out_Param: parameters_Doc_Out_Param);


            // сохранение документа
            document.Close();

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
