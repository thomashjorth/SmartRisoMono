using System;

namespace DataAggregator.Models.Configuration
{
	public class BarConfig : VisualizationConfig
	{
		public double ValueMin;
		public double ValueMax;
		public string Unit;
		public BarConfig ( string host, int port, 
			string aggregation, string parameters, 
			string titleHeading, double valueMin, 
			double valueMax, int updateIterval, 
			string unit) : base(host, port, 
				aggregation, parameters, titleHeading, 
				updateIterval)
		{
			VisualizationType = "d3Bar";
			ValueMin = valueMin;
			ValueMax = valueMax;
			Unit = unit;
		}
	}
}

