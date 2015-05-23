using System;
using System.Xml.Linq;
using System.Globalization;

namespace DataModel.Syslab
{
	public class CompositeMeasurement : CompositeData
	{
		public double value;

		public CompositeMeasurement () 
		{
			
		}
		public CompositeMeasurement (double val) 
		{
			value = val;
		}

		public CompositeMeasurement (string xml)
		{
			XDocument doc = XDocument.Parse(xml);

			if (doc.Root.Element ("value").Value == "0.0")
				value = 0;
			else {
				value = Math.Round (Double.Parse (
					doc.Root.Element ("value").Value.Replace (',', '.'), 
					CultureInfo.InvariantCulture
				), 2);
			}
			timestampMicros = long.Parse(doc.Root.Element	("timestampMicros").Value);
			timePrecision 	= short.Parse(doc.Root.Element	("timePrecision").Value);
			quality 		= byte.Parse(doc.Root.Element	("quality").Value);
			validity 		= byte.Parse(doc.Root.Element	("validity").Value);
			source 			= byte.Parse(doc.Root.Element	("source").Value);
		}

	}
}

