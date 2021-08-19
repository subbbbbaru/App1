using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App1.Droid;
using App1.Interface;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using App1;

[assembly: Xamarin.Forms.Dependency(typeof(FileService))]
namespace App1.Droid
{
    public class FileService : IFileService
    {
        public string GetRootPath()
        {
            return (string)Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments);
        }
        public void CreateFile()
        {
            var filename = "checkDateTim1e.pdf";
            var destination = System.IO.Path.Combine(GetRootPath(), filename);


            //создание PDF документа
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(new FileStream(destination, FileMode.Create, FileAccess.Write)));
            GeneratePDF generatePDF = new GeneratePDF();
            generatePDF.CreatePDFfile(pdfDocument);

        }
    }
}