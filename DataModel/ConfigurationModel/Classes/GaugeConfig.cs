using System;
using System.Collections.Generic;

namespace DataModel.ConfigurationModel.Classes
{
	public  class GaugeConfig : VisualizationConfig
	{
		public double ValueMin;
		public double ValueMax;
		public string ID, Unit;
		public string TitleHeading;

		public GaugeConfig (string host, int port, string device, string parameters, int updateIterval, string titleHeading, 
			double valueMin, double valueMax, string id, string unit) : base(host, port, device, parameters, updateIterval)
		{
			TitleHeading = titleHeading;
			ID = id;
			ValueMin = valueMin;
			ValueMax = valueMax;
			Unit = unit;
		}

	}

	public class GaugesConfig : VisualizationConfig{

		public List<GaugeConfig> Gauges;

		public GaugesConfig(GaugeConfig g1, GaugeConfig g2) : base(){
			VisualizationType = "d3Gauge";
			Gauges = new List<GaugeConfig>(){g1,g2};

		}

		public GaugesConfig(GaugeConfig g) : base(){
			VisualizationType = "d3Gauge";
			Gauges = new List<GaugeConfig>(){g};

		}

	}
}

