using System;

namespace DataAggregator
{
	public class SideBarConfig : VisualizationConfig
	{
		public SideBarConfig (string host, int port, string aggregation, string resource, string titleHeading) : base(host, port, aggregation, resource, titleHeading)
		{
			VisualizationType = "SideBar";
		}
	}
}

