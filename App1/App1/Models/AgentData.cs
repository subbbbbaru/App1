using System.Xml.Serialization;

namespace App1.Models
{
	public class AgentData
	{
		[XmlAttribute(AttributeName = "AgentOperation")]
		public string AgentOperation { get; set; }
		[XmlAttribute(AttributeName = "AgentPhone")]
		public string AgentPhone { get; set; }
		[XmlAttribute(AttributeName = "PaymentProcessorPhone")]
		public string PaymentProcessorPhone { get; set; }
		[XmlAttribute(AttributeName = "AcquirerOperatorPhone")]
		public string AcquirerOperatorPhone { get; set; }
		[XmlAttribute(AttributeName = "AcquirerOperatorName")]
		public string AcquirerOperatorName { get; set; }
		[XmlAttribute(AttributeName = "AcquirerOperatorAddress")]
		public string AcquirerOperatorAddress { get; set; }
		[XmlAttribute(AttributeName = "AcquirerOperatorINN")]
		public string AcquirerOperatorINN { get; set; }
	}
}