using System;
using System.Collections.Generic;

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

		public List<GaugeConfig> Gauges;

		public GaugesConfig(GaugeConfig g1, GaugeConfig g2) : base(){
			Gauges = new List<GaugeConfig>(){g1,g2};

		}

	}
}

