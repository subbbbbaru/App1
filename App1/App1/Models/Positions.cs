using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using App1.Models;

// CheckPackage Positions

namespace App1.Models
{
	public class Positions
	{
		[XmlElement(ElementName = "FiscalString")]
		public List<FiscalString> FiscalString { get; set; }

		[XmlAttribute(AttributeName = "TextString")]
		public string TextString { get; set; }

		[XmlElement(ElementName = "Barcode")]
		public List<Barcode> Barcode { get; set; }
	}

}
