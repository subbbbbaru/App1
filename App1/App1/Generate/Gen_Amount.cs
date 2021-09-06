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
    public class Gen_Amount
    {
        public void Gen_amount(Document document, CheckPackage parameters_Check_Pack, DocumentOutputParameters parameters_Doc_Out_Param, double price_DISCOUNT)
        {
            Table t_check_middle = new Table(UnitValue.CreatePercentArray(new float[] { 7, 3 })).UseAllAvailableWidth();


            double discount = 0.0;
            double discount_nacenka = 0.0;
            string discount_or_nacenka = "";

            foreach (var fiscalString in parameters_Check_Pack.Positions.FiscalString)
            {
                if (fiscalString.DiscountAmount != null)
                {
                    double temp_discount = double.Parse(fiscalString.DiscountAmount);
                    if (temp_discount > 0)
                    {
                        discount_nacenka = temp_discount + discount_nacenka;
                    }
                    else if (temp_discount < 0)
                    {
                        discount = discount + temp_discount;
                    }
                }
            }

            if ((discount_nacenka + discount) > 0)
            {
                discount_or_nacenka = "Наценка(всего):";
            }
            else
            {
                discount_or_nacenka = "Скидка(всего):";
            }


            // Скидка: Дисконтные карты ПРОСТО строка НАЦЕНКА или СКИДКА
            Paragraph p_Discount_string = new Paragraph(discount_or_nacenka);
            Cell cell_Discount_string = new Cell().Add(p_Discount_string)
                .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);


            // Скидка: Дисконтные карты СУММА скидок ИЛИ НАЦЕНОК
            Paragraph p_DiscountAmount = new Paragraph($"{discount_nacenka + discount}");
            Cell cell_DiscountAmount = new Cell().Add(p_DiscountAmount)
                .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
            t_check_middle.AddCell(cell_Discount_string);
            t_check_middle.AddCell(cell_DiscountAmount);


            // ИТОГ
            Paragraph p_Price_Amount = new Paragraph("ИТОГ:");
            Cell cell_Price_Amount = new Cell().Add(p_Price_Amount)
                .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);

            // ИТОГ(сумма)
            Paragraph p_Price_Discount = new Paragraph($"{price_DISCOUNT}");
            Cell cell_Price_Discount = new Cell().Add(p_Price_Discount)
                .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
            t_check_middle.AddCell(cell_Price_Amount);
            t_check_middle.AddCell(cell_Price_Discount);

            document.Add(t_check_middle);

            Table t_check_Payments = new Table(UnitValue.CreatePercentArray(new float[] { 1.5f, 5.5f, 3 })).UseAllAvailableWidth();


            // ОПЛАТА
            bool payments_flag = false; // проверка что первая ячейка(где ОПЛАТА) из трех нужна

            Paragraph p_check_product_Payments_string = new Paragraph($"ОПЛАТА: ");
            Cell cell_Payments_string = new Cell().Add(p_check_product_Payments_string)
                .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
            t_check_Payments.AddCell(cell_Payments_string);

            if (parameters_Check_Pack.Payments.Cash != null && parameters_Check_Pack.Payments.Cash != "")
            {
                payments_flag = true;
                string Cash = "НАЛИЧНЫМИ";
                Paragraph p_check_product_Payments_Cash = new Paragraph(Cash);
                Cell cell_Payments_Cash = new Cell().Add(p_check_product_Payments_Cash)
                    .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE);
                double g = double.Parse(parameters_Check_Pack.Payments.Cash);
                Paragraph p_check_product_Payments_Cash_amount = new Paragraph($"{g}");
                Cell cell_Payments_Cash_amount = new Cell().Add(p_check_product_Payments_Cash_amount)
                    .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE);
                t_check_Payments.AddCell(cell_Payments_Cash);
                t_check_Payments.AddCell(cell_Payments_Cash_amount);

            }
            if (parameters_Check_Pack.Payments.ElectronicPayment != null && parameters_Check_Pack.Payments.ElectronicPayment != "")
            {
                if (payments_flag == true)
                {
                    Paragraph space = new Paragraph("");
                    Cell cell_space = new Cell().Add(space)
                        .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetVerticalAlignment(VerticalAlignment.MIDDLE);
                    t_check_Payments.AddCell(cell_space);
                }

                string ElectronicPaymen = "БЕЗНАЛИЧНЫМИ";
                Paragraph p_check_product_Payments_ElectronicPayment = new Paragraph(ElectronicPaymen);
                Cell cell_Payments_ElectronicPayment = new Cell().Add(p_check_product_Payments_ElectronicPayment)
                    .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE);

                Paragraph p_check_product_Payments_ElectronicPayment_amount = new Paragraph($"{double.Parse(parameters_Check_Pack.Payments.ElectronicPayment)}");
                Cell cell_Payments_ElectronicPayment_amount = new Cell().Add(p_check_product_Payments_ElectronicPayment_amount)
                    .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE);
                t_check_Payments.AddCell(cell_Payments_ElectronicPayment);
                t_check_Payments.AddCell(cell_Payments_ElectronicPayment_amount);
                payments_flag = true;
            }
            if (parameters_Check_Pack.Payments.PrePayment != null && parameters_Check_Pack.Payments.PrePayment != "")
            {
                if (payments_flag == true)
                {
                    Paragraph space = new Paragraph("");
                    Cell cell_space = new Cell().Add(space)
                        .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetVerticalAlignment(VerticalAlignment.MIDDLE);
                    t_check_Payments.AddCell(cell_space);
                }
                string PrePayment = "ПРЕДОПЛАТА";
                Paragraph p_check_product_Payments_PrePayment = new Paragraph(PrePayment);
                Cell cell_Payments_PrePayment = new Cell().Add(p_check_product_Payments_PrePayment)
                    .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE);

                Paragraph p_check_product_Payments_PrePayment_amount = new Paragraph($"{double.Parse(parameters_Check_Pack.Payments.PrePayment)}");
                Cell cell_Payments_PrePayment_amount = new Cell().Add(p_check_product_Payments_PrePayment_amount)
                    .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE);
                t_check_Payments.AddCell(cell_Payments_PrePayment);
                t_check_Payments.AddCell(cell_Payments_PrePayment_amount);
                payments_flag = true;
            }
            if (parameters_Check_Pack.Payments.PostPayment != null && parameters_Check_Pack.Payments.PostPayment != "")
            {
                if (payments_flag == true)
                {
                    Paragraph space = new Paragraph("");
                    Cell cell_space = new Cell().Add(space)
                        .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetVerticalAlignment(VerticalAlignment.MIDDLE);
                    t_check_Payments.AddCell(cell_space);
                }
                string PostPayment = "ПОСТОПЛАТА";
                Paragraph p_check_product_Payments_PostPayment = new Paragraph(PostPayment);
                Cell cell_Payments_PostPayment = new Cell().Add(p_check_product_Payments_PostPayment)
                    .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE);

                Paragraph p_check_product_Payments_PostPayment_amount = new Paragraph($"{double.Parse(parameters_Check_Pack.Payments.PostPayment)}");
                Cell cell_Payments_PostPayment_amount = new Cell().Add(p_check_product_Payments_PostPayment_amount)
                    .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE);
                t_check_Payments.AddCell(cell_Payments_PostPayment);
                t_check_Payments.AddCell(cell_Payments_PostPayment_amount);
                payments_flag = true;
            }
            if (parameters_Check_Pack.Payments.Barter != null && parameters_Check_Pack.Payments.Barter != "")
            {
                if (payments_flag == true)
                {
                    Paragraph space = new Paragraph("");
                    Cell cell_space = new Cell().Add(space)
                        .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetVerticalAlignment(VerticalAlignment.MIDDLE);
                    t_check_Payments.AddCell(cell_space);
                }

                string Barter = "БАРТЕР";
                Paragraph p_check_product_Payments_Barter = new Paragraph(Barter);
                Cell cell_Payments_Barter = new Cell().Add(p_check_product_Payments_Barter)
                    .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE);

                Paragraph p_check_product_Payments_Barter_amount = new Paragraph($"{double.Parse(parameters_Check_Pack.Payments.Barter)}");
                Cell cell_Payments_Barter_amount = new Cell().Add(p_check_product_Payments_Barter_amount)
                    .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE);
                t_check_Payments.AddCell(cell_Payments_Barter);
                t_check_Payments.AddCell(cell_Payments_Barter_amount);
            }

            document.Add(t_check_Payments);
        }
    }
}
