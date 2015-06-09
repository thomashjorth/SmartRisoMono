using System;
using System.Collections.Generic;
using DataModel.ConfigurationModel.Classes;

namespace DataModel.ConfigurationModel.Pages
{
	public class ExperimentPageConfig : MasterPageConfig
	{

		public string HostAgg;
		public int PortAgg;
		public List<ExperimentConfig> Units = new List<ExperimentConfig>();

		public ExperimentPageConfig (List<ExperimentConfig> units, string pageType, string hostAgg, int portAgg) : base(pageType)
		{
			this.Units = units;
			this.HostAgg = hostAgg;
			this.PortAgg = portAgg;
		}
	}
}

