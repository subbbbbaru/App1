using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App1.Models;

using SkiaSharp;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            GetCheckPackage();
            
        }

        private void GetCheckPackage()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("App1.MyResources.CheckXML.xml");
            var serialize = new XmlSerializer(typeof(CheckPackage));
            CheckPackage parameters = (CheckPackage)serialize.Deserialize(stream);
            this.BindingContext = this;
           // MyListView.ItemsSource = parameters.Parameters.CashierINN; //ПОКА ЧТО НЕ РАБОТАЕТ
        }
		private bool isSupported = true;
		private void  Create_PDF(string pathh)
        {
            SKDocument document = SKDocument.CreatePdf("hghg");
			if (document == null)
			{
				isSupported = false;
				return;
			}

			SKPaint paint = new SKPaint
			{
				TextSize = 64.0f,
				IsAntialias = true,
				Color = 0xFF9CAFB7,
				IsStroke = true,
				StrokeWidth = 3,
				TextAlign = SKTextAlign.Center
			};

			var pageWidth = 840;
			var pageHeight = 1188;

			// draw page 1
			using (var pdfCanvas = document.BeginPage(pageWidth, pageHeight))
			{
				// draw button
				SKPaint nextPagePaint = new SKPaint
				{
					IsAntialias = true,
					TextSize = 16,
					Color = SKColors.OrangeRed
				};
				var nextText = "Next Page >>";
				var btn = new SKRect(pageWidth - nextPagePaint.MeasureText(nextText) - 24, 0, pageWidth, nextPagePaint.TextSize + 24);
				pdfCanvas.DrawText(nextText, btn.Left + 12, btn.Bottom - 12, nextPagePaint);
				// make button link
				pdfCanvas.DrawLinkDestinationAnnotation(btn, "next-page");

				// draw contents
				pdfCanvas.DrawText("...PDF 1/2...", pageWidth / 2, pageHeight / 4, paint);
				document.EndPage();
			}


		}

		private  void Button_Clicked(object sender, EventArgs e)
        {
			Create_PDF("gg");
        }
    }
}
