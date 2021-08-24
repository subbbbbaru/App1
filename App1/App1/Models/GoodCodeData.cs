using System.Xml.Serialization;

// CheckPackage GoodCodeData

namespace App1.Models
{
	public class GoodCodeData
	{
		[XmlAttribute(AttributeName = "MarkingCode")]
		public string MarkingCode { get; set; }
	}
}