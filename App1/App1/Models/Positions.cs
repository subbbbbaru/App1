using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using App1.Models;

namespace App1.Models
{
	public class Positions
	{
		[XmlElement(ElementName = "FiscalString")]
		public List<FiscalString> FiscalString { get; set; }
	}

}
