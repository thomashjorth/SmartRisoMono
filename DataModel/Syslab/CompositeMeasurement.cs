using System;

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

	}
}

