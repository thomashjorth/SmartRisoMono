using System;
using System.Collections.Generic;

namespace DataModel.ConfigurationModel.Classes
{
	public class PageConfig
	{
		public List<VisualizationConfig> Page;
		public string PageType;
		public PageConfig (List<VisualizationConfig> page, string pageType)
		{
			Page = page;
			PageType = pageType;
		}
	
	}
}

