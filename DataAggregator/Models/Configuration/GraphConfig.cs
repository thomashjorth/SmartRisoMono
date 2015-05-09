using System;

namespace DataAggregator.Models.Configuration
{
	public class GraphConfig : VisualizationConfig
	{
		public int XTicks, ValueMin, ValueMax, XLength;
		public VisualizationConfig DER;
		public string Unit;
		public GraphConfig ( string host, int port, string aggregation, string resource, string titleHeading, 
			int xTicks, int yMin, int yMax, int xLength, VisualizationConfig der, 
			int updateIterval, string unit) : base(host, port, aggregation, 
				resource,titleHeading, updateIterval)
		{
			VisualizationType = "d3Graph";
			XTicks = xTicks;
			ValueMin = yMin;
			ValueMax = yMax;
			XLength = xLength;
			DER = der;
			Unit = unit;
		}
	}
}

