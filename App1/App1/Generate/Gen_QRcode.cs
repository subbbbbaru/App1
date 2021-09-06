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

namespace App1.Generate
{
    public class Gen_QRcode
    {
        public void Footer_Check(Document document, PdfDocument pdfDoc, CheckPackage parameters_Check_Pack, DocumentOutputParameters parameters_Doc_Out_Param)
        {
            Table t_check_footer = new Table(UnitValue.CreatePercentArray(new float[] { 5, 5 })).UseAllAvailableWidth();


            // Эл. адр. отправителя
            Paragraph p_check_product_SenderEmail_string = new Paragraph("ЭЛ. АДР. ОТПРАВИТЕЛЯ:");
            Cell cell_SenderEmail_string = new Cell().Add(p_check_product_SenderEmail_string)
                .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
            Paragraph p_check_product_SenderEmail = new Paragraph($"{parameters_Check_Pack.Parameters.SenderEmail}");
            Cell cell_SenderEmail = new Cell().Add(p_check_product_SenderEmail)
                .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
            t_check_footer.AddCell(cell_SenderEmail_string);
            t_check_footer.AddCell(cell_SenderEmail);

            // САЙТ ФНС
            Paragraph p_check_product_AddressSiteInspections_string = new Paragraph("САЙТ ФНС:");
            Cell cell_AddressSiteInspections_string = new Cell().Add(p_check_product_AddressSiteInspections_string)
                .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
            Paragraph p_check_product_AddressSiteInspections = new Paragraph($"{parameters_Doc_Out_Param.Parameters.AddressSiteInspections}");
            Cell cell_AddressSiteInspections = new Cell().Add(p_check_product_AddressSiteInspections)
                .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
            t_check_footer.AddCell(cell_AddressSiteInspections_string);
            t_check_footer.AddCell(cell_AddressSiteInspections);

            document.Add(t_check_footer);


            //добавить QR-код в приложение и установить местоположение
            Paragraph paragraph2 = new Paragraph();
            paragraph2.SetTextAlignment(TextAlignment.CENTER).Add(CreateQRcode(pdfDoc, "good"));
            document.Add(paragraph2);


            // ФД и ФП + название фискального регистратора(пример: СП101-Ф)
            Table t_check_footer_down = new Table(UnitValue.CreatePercentArray(new float[] { 6, 4 })).UseAllAvailableWidth();
            Paragraph p_check_product_FD_FP = new Paragraph($"ФД:{parameters_Doc_Out_Param.Parameters.CheckNumber} ФП: {parameters_Doc_Out_Param.Parameters.FiscalSign}");
            Cell cell_FD_FP = new Cell().Add(p_check_product_FD_FP)
                .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
            Paragraph p_check_product_NameFiskReg = new Paragraph($"Какой-то ФР");
            Cell cell_NameFiskReg = new Cell().Add(p_check_product_NameFiskReg)
                .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);

            t_check_footer_down.AddCell(cell_FD_FP);
            t_check_footer_down.AddCell(cell_NameFiskReg);
            document.Add(t_check_footer_down);
        }

        // Генерация QR code
        iText.Layout.Element.Image CreateQRcode(PdfDocument pdfDoc, string text)
        {
            //создание QR-кода
            BarcodeQRCode barcodeQRCode = new BarcodeQRCode(text);
            //конвертация QR-кода в изображение
            var imgBar = new iText.Layout.Element.Image(barcodeQRCode.CreateFormXObject(pdfDoc));
            imgBar.SetWidth(56.6929f);
            imgBar.SetHeight(56.6929f);
            return imgBar;
        }
    }
}
