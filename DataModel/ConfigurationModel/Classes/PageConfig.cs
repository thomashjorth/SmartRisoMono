using System;
using System.Collections.Generic;

namespace DataModel.ConfigurationModel.Classes
{
	public class PageConfig
	{
		public List<VisualizationConfig> Page;
		public PageConfig (List<VisualizationConfig> page)
		{
			Page = page;
		}
	
	}
}

