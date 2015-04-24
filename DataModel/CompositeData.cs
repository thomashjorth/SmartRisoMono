using System;

namespace DataModel
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
	}
}

