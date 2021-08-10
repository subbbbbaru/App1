using System.Xml.Serialization;

namespace App1.Models
{
	public class CorrectionData
	{
		[XmlAttribute(AttributeName = "Type")]
		public string Type { get; set; }
		[XmlAttribute(AttributeName = "Description")]
		public string Description { get; set; }
		[XmlAttribute(AttributeName = "Datе")]
		public string Datе { get; set; }
		[XmlAttribute(AttributeName = "Number")]
		public string Number { get; set; }
	}
}