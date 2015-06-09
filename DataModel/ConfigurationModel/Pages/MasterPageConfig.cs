using System;

namespace DataModel.ConfigurationModel.Pages
{
	public abstract class MasterPageConfig
	{

		public string PageType;

		public MasterPageConfig (string pageType)
		{
			this.PageType = pageType;
		}	
	}
}

