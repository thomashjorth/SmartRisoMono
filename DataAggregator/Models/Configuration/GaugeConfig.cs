using System;

namespace DataAggregator
{
	public class GaugeConfig : VisualizationConfig
	{
		public double ValueMin;
		public double ValueMax;
		public string ID;
		public GaugeConfig ( string host, int port, string aggregation, string resource, string titleHeading, double valueMin, double valueMax, string id) : base(host, port, aggregation, resource, titleHeading)
		{
			ID = id;
			VisualizationType = "d3Gauge";
			ValueMin = valueMin;
			ValueMax = valueMax;
		}
	}
}

