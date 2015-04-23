using System;

namespace DataAggregator
{
	public  class GaugeConfig : VisualizationConfig
	{
		public double ValueMin;
		public double ValueMax;
		public string ID;
		public VisualizationConfig DER;

		public GaugeConfig ( string host, int port, string aggregation, string resource, string titleHeading, double valueMin, double valueMax, string id, VisualizationConfig der) : base(host, port, aggregation, resource, titleHeading)
		{
			DER = der;
			ID = id;
			VisualizationType = "d3Gauge";
			ValueMin = valueMin;
			ValueMax = valueMax;
		}

	}

	public class GaugesConfig : VisualizationConfig{
		public GaugeConfig Gauge1;
		public GaugeConfig Gauge2;
		public GaugesConfig(GaugeConfig g1, GaugeConfig g2) : base(){
			Gauge1 = g1;
			Gauge2 = g2;
		}

	}
}

