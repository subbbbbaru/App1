using App1.Interface;
using App1.iOS;
using Foundation;
using iText.Kernel.Pdf;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(FileService))]
namespace App1.iOS
{
    public class FileService : IFileService
    {
        public string GetRootPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }
        public void CreateFile()
        {
            var filename = "checkDateTime.pdf";
            var destination = System.IO.Path.Combine(GetRootPath(), filename);


            //создание PDF документа
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(new FileStream(destination, FileMode.Create, FileAccess.Write)));
            GeneratePDF generatePDF = new GeneratePDF();
            generatePDF.CreatePDFfile(pdfDocument);

        }
    }
}