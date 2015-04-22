using System;

namespace DataAggregator
{
	public class GraphConfig : VisualizationConfig
	{
		public int XTicks, YMin, YMax;
		public GraphConfig ( string host, int port, string aggregation, string resource, string titleHeading, int xTicks, int yMin, int yMax) : base(host, port, aggregation, resource,titleHeading)
		{
			VisualizationType = "d3Graph";
			XTicks = xTicks;
			YMin = yMin;
			YMax = yMax;
		}
	}
}

