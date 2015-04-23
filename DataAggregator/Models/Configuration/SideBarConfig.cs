using System;

namespace DataAggregator
{
	public class SideBarConfig : VisualizationConfig
	{
		public SideBarConfig (string host, int port, string aggregation, string resource, string titleHeading, int updateIterval) : base(host, port, aggregation, resource, titleHeading, updateIterval)
		{
			VisualizationType = "SideBar";
		}
	}
}

