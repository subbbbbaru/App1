using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace App1.Models
{
	public class FiscalString
	{
		[XmlElement(ElementName = "AgentData")]
		public AgentData AgentData { get; set; }
		[XmlElement(ElementName = "VendorData")]
		public VendorData VendorData { get; set; }
		[XmlElement(ElementName = "GoodCodeData")]
		public GoodCodeData GoodCodeData { get; set; }
		[XmlAttribute(AttributeName = "Name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName = "Quantity")]
		public string Quantity { get; set; }
		[XmlAttribute(AttributeName = "PriceWithDiscount")]
		public string PriceWithDiscount { get; set; }
		[XmlAttribute(AttributeName = "AmountWithDiscount")]
		public string AmountWithDiscount { get; set; }
		[XmlAttribute(AttributeName = "DiscountAmount")]
		public string DiscountAmount { get; set; }
		[XmlAttribute(AttributeName = "Department")]
		public string Department { get; set; }
		[XmlAttribute(AttributeName = "VATRate")]
		public string VATRate { get; set; }
		[XmlAttribute(AttributeName = "VATAmount")]
		public string VATAmount { get; set; }
		[XmlAttribute(AttributeName = "PaymentMethod")]
		public string PaymentMethod { get; set; }
		[XmlAttribute(AttributeName = "CalculationSubject")]
		public string CalculationSubject { get; set; }
		[XmlAttribute(AttributeName = "CalculationAgent")]
		public string CalculationAgent { get; set; }
		[XmlAttribute(AttributeName = "MeasurementUnit")]
		public string MeasurementUnit { get; set; }
		[XmlAttribute(AttributeName = "CountryOfOrigin")]
		public string CountryOfOrigin { get; set; }
		[XmlAttribute(AttributeName = "CustomsDeclaration")]
		public string CustomsDeclaration { get; set; }
		[XmlAttribute(AttributeName = "AdditionalAttribute")]
		public string AdditionalAttribute { get; set; }
		[XmlAttribute(AttributeName = "ExciseAmount")]
		public string ExciseAmount { get; set; }
	}
}
