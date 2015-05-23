using System;
using System.Xml.Linq;

namespace DataModel.Syslab
{
	public class Status : CompositeData
	{
		public int status;

		public Status () 
		{

		}
		public Status (int val) 
		{
			status = val;
		}

		public Status (string xml)
		{
			XDocument doc = XDocument.Parse(xml);

			status 			= int.Parse(doc.Root.Element("status").Element("status").Value);
			timestampMicros = long.Parse(doc.Root.Element	("timestampMicros").Value);
			timePrecision 	= short.Parse(doc.Root.Element	("timePrecision").Value);
			quality 		= byte.Parse(doc.Root.Element	("quality").Value);
			validity 		= byte.Parse(doc.Root.Element	("validity").Value);
			source 			= byte.Parse(doc.Root.Element	("source").Value);
		}

	}
}

