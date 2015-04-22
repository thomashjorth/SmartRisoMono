using System;

namespace DataAggregator
{
	public class GaugeConfig : VisualizationConfig
	{
		double ValueMin;
		double ValueMax;
		public GaugeConfig ( string host, int port, string aggregation, string resource, string titleHeading, double valueMin, double valueMax) : base(host, port, aggregation, resource, titleHeading)
		{
			VisualizationType = "d3Gauge";
			ValueMin = valueMin;
			ValueMax = valueMax;
		}
	}
}

