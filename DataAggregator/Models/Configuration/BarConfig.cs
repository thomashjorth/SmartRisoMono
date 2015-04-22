using System;

namespace DataAggregator
{
	public class BarConfig : VisualizationConfig
	{
		double ValueMin;
		double ValueMax;
		public BarConfig ( string host, int port, string aggregation, string resource, string titleHeading, double valueMin, double valueMax) : base(host, port, aggregation, resource, titleHeading)
		{
			VisualizationType = "d3Bar";
			ValueMin = valueMin;
			ValueMax = valueMax;
		}
	}
}

