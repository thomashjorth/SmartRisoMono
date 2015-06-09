using System;
using System.Collections.Generic;
using DataModel.ConfigurationModel.Classes;
using DataModel.ConfigurationModel.Pages;

namespace DataModel.ConfigurationModel
{
	public class PagesConfig
	{
		public List<MasterPageConfig> Pages = new List<MasterPageConfig>(){};
		public PagesConfig (List<MasterPageConfig> pages)
		{
			Pages = pages;
		}

		public void addPagesConfig(PagesConfig pages){
			Pages.AddRange (pages.Pages);
		}
	}
}

