using System;
using System.Xml.Linq;

namespace DataModel.Syslab
{
	public class CompositeData
	{
		/** timestamp of measurement as epoch value with microsecond precision */
		public long timestampMicros;
		/** precision of timestamp */
		public short timePrecision;
		/**
   		* quality of measurement, i.e. OK or another ot the {@link Quality} enum
   		* values
   		*/
		public byte quality;
		/**
   		* quality of measurement, i.e. OK or another ot the {@link Quality} enum
   		* values
   		*/
		public byte validity;
		/**
   		* source of measurement, i.e. Process (i.e. real data) or Substitute (e.g.
   		* simulated data)
   		*/
		public byte source;

		public CompositeData ()
		{
		}

		public CompositeData (string xml)
		{
			XDocument doc = XDocument.Parse(xml);

			timestampMicros = long.Parse(doc.Root.Element	("timestampMicros").Value);
			timePrecision 	= short.Parse(doc.Root.Element	("timePrecision").Value);
			quality 		= byte.Parse(doc.Root.Element	("quality").Value);
			validity 		= byte.Parse(doc.Root.Element	("validity").Value);
			source 			= byte.Parse(doc.Root.Element	("source").Value);
		}
	}
}

