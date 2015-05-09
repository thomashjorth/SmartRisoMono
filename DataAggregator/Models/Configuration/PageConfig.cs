using System;
using System.Collections.Generic;

namespace DataAggregator.Models.Configuration
{
	public class PageConfig
	{
		public List<VisualizationConfig> Page = new List<VisualizationConfig>(){};
		public PageConfig (List<VisualizationConfig> page)
		{
			Page = page;
		}
	}
}

