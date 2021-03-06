﻿using System;

namespace DataModel.ConfigurationModel.Classes
{
	public class BarConfig : VisualizationConfig
	{
		public double ValueMin;
		public double ValueMax;
		public string Unit;
		public string TitleHeading;
		public string VisualizationType;
		public BarConfig ( string host, int port, string device, string parameters, int updateIterval,
			string titleHeading, double valueMin, double valueMax, string unit)
			: base(host, port, device, parameters,	updateIterval)
		{
			VisualizationType = "d3Bar";
			TitleHeading = titleHeading;
			ValueMin = valueMin;
			ValueMax = valueMax;
			Unit = unit;
		}
	}
}

