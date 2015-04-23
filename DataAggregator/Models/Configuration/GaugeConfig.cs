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
		public string Unit;
		public GaugeConfig (string unit, double valueMin, double valueMax, string id, VisualizationConfig der)
		{
			Unit = unit;
			DER = der;
			ID = id;

			ValueMin = valueMin;
			ValueMax = valueMax;
		}

	}

	public class GaugesConfig : VisualizationConfig{

		public List<GaugeConfig> Gauges;

		public GaugesConfig( string host, int port, string aggregation, string resource, string titleHeading, GaugeConfig g1, GaugeConfig g2) : base(host, port, aggregation, resource, titleHeading){
			VisualizationType = "d3Gauge";
			Gauges = new List<GaugeConfig>(){g1,g2};

		}

	}
}

