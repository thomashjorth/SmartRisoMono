using System;

namespace DataAggregator
{
	public class PieConfig : VisualizationConfig
	{
		
		public PieConfig ( string host, int port, string aggregation, string resource, string titleHeading, int updateIterval) : base(host, port, aggregation, resource, titleHeading, updateIterval)
		{
			VisualizationType = "d3Pie";
		}
	}
}

