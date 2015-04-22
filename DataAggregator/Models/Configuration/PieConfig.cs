using System;

namespace DataAggregator
{
	public class PieConfig : VisualizationConfig
	{
		
		public PieConfig ( string host, int port, string aggregation, string resource, string titleHeading) : base(host, port, aggregation, resource, titleHeading)
		{
			VisualizationType = "d3Pie";
		}
	}
}

