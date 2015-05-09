using System;

namespace DataAggregator.Models.Configuration
{
	public class PieConfig : VisualizationConfig
	{
		
		public PieConfig ( string host, int port, string aggregation, string parameters, string titleHeading, int updateIterval) : base(host, port, aggregation, parameters, titleHeading, updateIterval)
		{
			VisualizationType = "d3Pie";
		}
	}
}

