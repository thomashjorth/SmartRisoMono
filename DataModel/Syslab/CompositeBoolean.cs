using System;
using System.Xml.Linq;

namespace DataModel.Syslab
{
	public class CompositeBoolean : CompositeData
	{
		public bool value;

		public CompositeBoolean () 
		{

		}
		public CompositeBoolean (bool val) 
		{
			value = val;
		}

		public CompositeBoolean (string xml)
		{
			XDocument doc = XDocument.Parse(xml);

			value 			= doc.Root.Element("value").Value == "true";
			timestampMicros = long.Parse(doc.Root.Element	("timestampMicros").Value);
			timePrecision 	= short.Parse(doc.Root.Element	("timePrecision").Value);
			quality 		= byte.Parse(doc.Root.Element	("quality").Value);
			validity 		= byte.Parse(doc.Root.Element	("validity").Value);
			source 			= byte.Parse(doc.Root.Element	("source").Value);
		}

	}
}

