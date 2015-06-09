using System;
using System.Collections.Generic;

namespace DataModel.ConfigurationModel.Pages
{
	public class PageConfig : MasterPageConfig
	{
		public List<VisualizationConfig> Page;

		public PageConfig (List<VisualizationConfig> page, string pageType) : base(pageType)
		{
			Page = page;
		}
	
	}
}

