using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace App1.Models
{
	[XmlRoot(ElementName = "DocumentOutputParameters")]
	public class DocumentOutputParameters
	{
		[XmlElement(ElementName = "Parameters")]
		public Parameters_Doc_Out_Param Parameters { get; set; }

	}
}
