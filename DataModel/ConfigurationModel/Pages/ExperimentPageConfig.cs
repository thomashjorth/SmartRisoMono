using System;
using System.Collections.Generic;
using DataModel.ConfigurationModel.Classes;

namespace DataModel.ConfigurationModel.Pages
{
	public class ExperimentPageConfig : MasterPageConfig
	{

		public List<ExperimentConfig> Units = new List<ExperimentConfig>();

		public ExperimentPageConfig (List<ExperimentConfig> units, string pageType) : base(pageType)
		{
			this.Units = units;
		}
	}
}

