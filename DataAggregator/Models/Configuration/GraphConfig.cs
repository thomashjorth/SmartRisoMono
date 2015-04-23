using System;

namespace DataAggregator
{
	public class GraphConfig : VisualizationConfig
	{
		public int XTicks, ValueMin, ValueMax, XLength;
		public GraphConfig ( string host, int port, string aggregation, string resource, string titleHeading, int xTicks, int yMin, int yMax, int xLength) : base(host, port, aggregation, resource,titleHeading)
		{
			VisualizationType = "d3Graph";
			XTicks = xTicks;
			ValueMin = yMin;
			ValueMax = yMax;
			XLength = xLength;
		}
	}
}

