using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

// CheckPackage PARAMETERS

namespace App1.Models
{
	public class Parameters
	{
		[XmlElement(ElementName = "CorrectionData")]
		public CorrectionData CorrectionData { get; set; }
		[XmlElement(ElementName = "AgentData")]
		public AgentData AgentData { get; set; }
		[XmlElement(ElementName = "VendorData")]
		public VendorData VendorData { get; set; }
		[XmlElement(ElementName = "UserAttribute")]
		public UserAttribute UserAttribute { get; set; }
		[XmlAttribute(AttributeName = "CashierName")]
		public string CashierName { get; set; }
		[XmlAttribute(AttributeName = "CashierINN")]
		public string CashierINN { get; set; }
		[XmlAttribute(AttributeName = "OperationType")]
		public string OperationType { get; set; }
		[XmlAttribute(AttributeName = "TaxationSystem")]
		public string TaxationSystem { get; set; }
		[XmlAttribute(AttributeName = "CustomerInfo")]
		public string CustomerInfo { get; set; }
		[XmlAttribute(AttributeName = "CustomerINN")]
		public string CustomerINN { get; set; }
		[XmlAttribute(AttributeName = "CustomerEmail")]
		public string CustomerEmail { get; set; }
		[XmlAttribute(AttributeName = "CustomerPhone")]
		public string CustomerPhone { get; set; }
		[XmlAttribute(AttributeName = "SenderEmail")]
		public string SenderEmail { get; set; }
		[XmlAttribute(AttributeName = "SaleAddress")]
		public string SaleAddress { get; set; }
		[XmlAttribute(AttributeName = "SaleLocation")]
		public string SaleLocation { get; set; }
		[XmlAttribute(AttributeName = "AgentType")]
		public string AgentType { get; set; }
		[XmlAttribute(AttributeName = "AdditionalAttribute")]
		public string AdditionalAttribute { get; set; }
	}

}
