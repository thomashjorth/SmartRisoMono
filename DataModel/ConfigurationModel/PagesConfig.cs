using System;
using System.Collections.Generic;
using DataModel.ConfigurationModel.Classes;

namespace DataModel.ConfigurationModel
{
	public class PagesConfig
	{
		public List<PageConfig> Pages = new List<PageConfig>(){};
		public PagesConfig (List<PageConfig> pages)
		{
			Pages = pages;
		}
	}
}

