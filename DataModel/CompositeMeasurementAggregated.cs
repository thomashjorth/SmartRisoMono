using System;
using DataModel.Syslab;

namespace DataModel
{
	public class CompositeMeasurementAggregated : CompositeMeasurement
	{
		public double avg;
		public int count;

		public CompositeMeasurementAggregated () 
		{
			
		}
		public CompositeMeasurementAggregated (double total, double avg, int count) : base(total)
		{
			this.avg = avg;
			this.count = count;
		}

	}
}

