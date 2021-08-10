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
    }
}
