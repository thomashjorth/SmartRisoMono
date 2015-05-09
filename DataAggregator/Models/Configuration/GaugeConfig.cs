﻿using System;
using System.Collections.Generic;

namespace DataAggregator.Models.Configuration
{
	public  class GaugeConfig : VisualizationConfig
	{
		public double ValueMin;
		public double ValueMax;
		public string ID, Unit;
		public VisualizationConfig DER;

		public GaugeConfig (string host, int port, string aggregation, string resource, string titleHeading, 
			double valueMin, double valueMax, string id, VisualizationConfig der, int updateIterval, string unit) : base(host, port, aggregation, resource, titleHeading, updateIterval)
		{
			DER = der;
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

	}
}

