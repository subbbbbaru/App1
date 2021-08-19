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
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using iText.Layout;
using iText.Kernel.Font;
using iText.IO.Font;
using iText.Layout.Element;
using iText.Kernel.Colors;
using App1.Interface;
using Xamarin.Essentials;



namespace App1
{
    public partial class MainPage : ContentPage 
    {
        public MainPage()
        {
            //DependencyService.Register<IReadWritePermission, ReadWriteStoragePermission>();
            //var readWritePermission = DependencyService.Get<IReadWritePermission>();
            

            InitializeComponent();

            GetCheckPackage();
            
        }

        private void GetCheckPackage()
        {
            //var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            //Stream stream = assembly.GetManifestResourceStream("App1.MyResources.CheckXML.xml");
            //var serialize = new XmlSerializer(typeof(CheckPackage));
            //CheckPackage parameters = (CheckPackage)serialize.Deserialize(stream);
            //this.BindingContext = this;
           // MyListView.ItemsSource = parameters.Parameters.CashierINN; //ПОКА ЧТО НЕ РАБОТАЕТ
        }


		private async void Button_Clicked(object sender, EventArgs e)
        {
            var permission = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

            if (permission != PermissionStatus.Granted)
            {
                permission = await Permissions.RequestAsync<Permissions.StorageWrite>();
            }
            if (permission != PermissionStatus.Granted)
            {
                return;
            }
            DependencyService.Get<IFileService>().CreateFile();

        }
    }
}
