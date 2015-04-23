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

	public class GaugesConfig : VisualizationConfig{
		public GaugeConfig Gauge1;
		public GaugeConfig Gauge2;
		public GaugesConfig(GaugeConfig g1, GaugeConfig g2, string titleHeading) : base(titleHeading){
			Gauge1 = g1;
			Gauge2 = g2;
		}

	}
}

