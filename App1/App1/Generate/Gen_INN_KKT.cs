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
    public class Gen_INN_KKT
    {
        public void Gen_INN_kkt(Document document, CheckPackage parameters_Check_Pack, DocumentOutputParameters parameters_Doc_Out_Param)
        {



            Table t_check_header = new Table(UnitValue.CreatePercentArray(new float[] { 7, 3 })).UseAllAvailableWidth();

            // ИНН
            Paragraph p_parameters_CashierINN = new Paragraph($"ИНН:{parameters_Check_Pack.Parameters.CashierINN} кассира?");
            Cell cell_CashierINN = new Cell().Add(p_parameters_CashierINN)
                .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
            t_check_header.AddCell(cell_CashierINN);


            // ФН
            Paragraph p_FN = new Paragraph("ФН: ХЧЧЩЩЩЩ");
            Cell cell_FN = new Cell().Add(p_FN)
                .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
            t_check_header.AddCell(cell_FN);

            // РН ККТ
            Paragraph p_RN_KKT = new Paragraph("РН ККТ: ХХХХХХЫЧ");
            Cell cell_RN_KKT = new Cell().Add(p_RN_KKT)
                .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
            t_check_header.AddCell(cell_RN_KKT);

            // СНО
            Paragraph p_parameters_TaxationSystem = new Paragraph($"СНО:{check_TaxationSystem(parameters_Check_Pack.Parameters.TaxationSystem)}");
            Cell cell_TaxationSystem = new Cell().Add(p_parameters_TaxationSystem)
                .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
            t_check_header.AddCell(cell_TaxationSystem);

            // ЗН ККТ
            Paragraph p_ZN_KKT = new Paragraph("ЗН ККТ: ХХХХХХЫЧ");
            Cell cell_ZN_KKT = new Cell().Add(p_ZN_KKT)
                .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
            t_check_header.AddCell(cell_ZN_KKT);

            // СМЕНА и ЧЕК за смену
            Paragraph p_doc_out_par_ShiftNumber_CheckNumber = new Paragraph($"СМЕНА:{parameters_Doc_Out_Param.Parameters.ShiftNumber} ЧЕК:{parameters_Doc_Out_Param.Parameters.ShiftClosingCheckNumber}");
            Cell cell_doc_out_par_ShiftNumber_CheckNumber = new Cell().Add(p_doc_out_par_ShiftNumber_CheckNumber)
                .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
            t_check_header.AddCell(cell_doc_out_par_ShiftNumber_CheckNumber);

            // МЕСТО РАСЧЕТОВ
            Paragraph p_parameters_SaleAddress_string = new Paragraph($"МЕСТО РАСЧЕТОВ(тут адрес или место?):");
            Cell cell_SaleAddress_string = new Cell().Add(p_parameters_SaleAddress_string)
                .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
            t_check_header.AddCell(cell_SaleAddress_string);

            Paragraph p_parameters_SaleAddress = new Paragraph($"МЕСТО РАСЧЕТОВ(тут адрес или место?):{parameters_Check_Pack.Parameters.SaleAddress}");
            Cell cell_SaleAddress = new Cell().Add(p_parameters_SaleAddress)
                .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
            t_check_header.AddCell(cell_SaleAddress);

            // КАССИР
            Paragraph p_parameters_CashierName = new Paragraph($"КАССИР:{parameters_Check_Pack.Parameters.CashierName}");
            Cell cell_CashierName = new Cell().Add(p_parameters_CashierName)
                .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
            t_check_header.AddCell(cell_CashierName);

            // ДАТА
            Paragraph p_doc_out_par_DateTime = new Paragraph(parameters_Doc_Out_Param.Parameters.DateTime);
            Cell cell_doc_out_par_DateTime = new Cell().Add(p_doc_out_par_DateTime)
                .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
            t_check_header.AddCell(cell_doc_out_par_DateTime);


            // ПРЕДЛОЖЕНИЕ ОТ АНДРЕЯ (Меня заставили)






            document.Add(t_check_header);
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
    }
}
