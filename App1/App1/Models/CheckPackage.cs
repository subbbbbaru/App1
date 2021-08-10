using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;


namespace App1.Models
{

	[XmlRoot(ElementName = "CheckPackage")]
	public class CheckPackage
	{
		[XmlElement(ElementName = "Parameters")]
		public Parameters Parameters { get; set; }
		[XmlElement(ElementName = "Positions")]
		public Positions Positions { get; set; }
		[XmlElement(ElementName = "Payments")]
		public Payments Payments { get; set; }
	}

}
