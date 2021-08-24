using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

// CheckPackage Payments

namespace App1.Models
{
	public class Payments
	{
		[XmlAttribute(AttributeName = "Cash")]
		public string Cash { get; set; }
		[XmlAttribute(AttributeName = "ElectronicPayment")]
		public string ElectronicPayment { get; set; }
		[XmlAttribute(AttributeName = "PrePayment")]
		public string PrePayment { get; set; }
		[XmlAttribute(AttributeName = "PostPayment")]
		public string PostPayment { get; set; }
		[XmlAttribute(AttributeName = "Barter")]
		public string Barter { get; set; }
	}

}
