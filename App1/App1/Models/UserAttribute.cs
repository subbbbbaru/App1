using System.Xml.Serialization;

// CheckPackage UserAttribute

namespace App1.Models
{
	public class UserAttribute
	{
		[XmlAttribute(AttributeName = "Name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName = "Value")]
		public string Value { get; set; }
	}
}