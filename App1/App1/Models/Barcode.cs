using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace App1.Models
{
    public class Barcode
    {
		[XmlAttribute(AttributeName = "Type")]
		public string Type { get; set; }
		[XmlAttribute(AttributeName = "ValueBase64")]
		public string ValueBase64 { get; set; }
	}
}
