using System.Xml.Serialization;

// CheckPackage VendorData

namespace App1.Models
{
	public class VendorData
	{
		[XmlAttribute(AttributeName = "VendorPhone")]
		public string VendorPhone { get; set; }
		[XmlAttribute(AttributeName = "VendorName")]
		public string VendorName { get; set; }
		[XmlAttribute(AttributeName = "VendorINN")]
		public string VendorINN { get; set; }
	}
}