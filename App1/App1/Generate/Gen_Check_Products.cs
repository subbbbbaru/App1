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
    public class Gen_Check_Products
    {
        public void Gen_Check_Product(Document document, CheckPackage parameters_Check_Pack, DocumentOutputParameters parameters_Doc_Out_Param, LineSeparator lineSeparator)
        {
            Paragraph p_Title_check = new Paragraph($"КАССОВЫЙ ЧЕК {check_OperationType(parameters_Check_Pack.Parameters.OperationType)}")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(14);
            document.Add(p_Title_check);


            double price_DISCOUNT = 0.0;
            Table t_check_product = new Table(UnitValue.CreatePercentArray(new float[] { 7, 3 })).UseAllAvailableWidth();
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
                price_DISCOUNT = double.Parse(fiscalString.PriceWithDiscount) + price_DISCOUNT;


                // ячейка с НДС товара
                if (fiscalString.VATRate != null)
                {
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
                        .SetVerticalAlignment(VerticalAlignment.BOTTOM);

                    // Добавление ячейки Имени и цены в таблицу
                    t_check_product.AddCell(cell_Name);
                    t_check_product.AddCell(cell_PriceWithDiscount);

                    // Добавление ячейки НДС и Признаком способа расчета в таблицу
                    t_check_product.AddCell(cell_VATRate);
                    t_check_product.AddCell(cell_PaymentMethod);
                }
                else
                {
                    Paragraph p_check_product_VATRate = new Paragraph($"");
                    Cell cell_VATRate = new Cell().Add(p_check_product_VATRate)
                        .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetVerticalAlignment(VerticalAlignment.BOTTOM);

                    // ячейка с Признаком способа расчета
                    Paragraph p_check_product_PaymentMethod = new Paragraph(check_PaymentMethod(fiscalString.PaymentMethod));
                    Cell cell_PaymentMethod = new Cell().Add(p_check_product_PaymentMethod)
                        .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                        .SetTextAlignment(TextAlignment.RIGHT)
                        .SetVerticalAlignment(VerticalAlignment.BOTTOM);

                    // Добавление ячейки Имени и цены в таблицу
                    t_check_product.AddCell(cell_Name);
                    t_check_product.AddCell(cell_PriceWithDiscount);

                    // Добавление ячейки НДС и Признаком способа расчета в таблицу
                    t_check_product.AddCell(cell_VATRate);
                    t_check_product.AddCell(cell_PaymentMethod);
                }
            }
            // Добавить таблицу в документ
            document.Add(t_check_product);
            document.Add(lineSeparator);

            // Генерация блока Суммы и Итога
            Gen_Amount gen_Amount = new Gen_Amount();
            gen_Amount.Gen_amount(document, parameters_Check_Pack, parameters_Doc_Out_Param, price_DISCOUNT);

        }

        // Тип операции (Таблица 25 документа ФФД)
        private string check_OperationType(string OperationType)
        {
            string operType = OperationType;
            string operType_return = "";
            switch (operType)
            {
                case "1":
                    operType_return = "ПРИХОД";
                    break;
                case "2":
                    operType_return = "ВОЗВРАТ ПРИХОДА";
                    break;
                case "3":
                    operType_return = "РАСХОД";
                    break;
                case "4":
                    operType_return = "ВОЗВРАТ РАСХОДА";
                    break;
                default:
                    operType_return = "ERROR !!! Тип операции!";
                    break;
            }
            return operType_return;
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

    }
}
