﻿using System;

namespace DataModel.ConfigurationModel.Classes
{
	public class GraphConfig : VisualizationConfig
	{
		public int XLength;
		public double ValueMin, ValueMax;
		public string TitleHeading;
		public string Unit;
		public GraphConfig ( string host, int port, string device, string parameters, int updateIterval, 
			string titleHeading, int yMin, int yMax, int xLength, string unit)
			: base(host, port, device, parameters, updateIterval)
		{
			VisualizationType = "d3Graph";
			TitleHeading = titleHeading;
			ValueMin = yMin;
			ValueMax = yMax;
			XLength = xLength;
			Unit = unit;
		}
	}
}

