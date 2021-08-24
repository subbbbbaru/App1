using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace App1.Models
{
    public class Parameters_Doc_Out_Param
    {
		[XmlAttribute(AttributeName = "	ShiftNumber")]
		public string ShiftNumber { get; set; }
		[XmlAttribute(AttributeName = "CheckNumber")]
		public string CheckNumber { get; set; }
		[XmlAttribute(AttributeName = "ShiftClosingCheckNumber")]
		public string ShiftClosingCheckNumber { get; set; }
		[XmlAttribute(AttributeName = "AddressSiteInspections")]
		public string AddressSiteInspections { get; set; }
		[XmlAttribute(AttributeName = "FiscalSign")]
		public string FiscalSign { get; set; }
		[XmlAttribute(AttributeName = "DateTime")]
		public string DateTime { get; set; }
	}
}
