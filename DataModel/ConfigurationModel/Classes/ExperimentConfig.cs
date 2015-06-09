using System;
using System.Collections.Generic;

namespace DataModel.ConfigurationModel.Classes
{
	public class ExperimentConfig : VisualizationConfig
	{
		public Guid Id;
		public VisualizationConfig Visualization1;
		public VisualizationConfig Visualization2;

		public ExperimentConfig (string host, int port, string device, 
		                         string parameters, int updateInterval, VisualizationConfig visualization1, 
		                         VisualizationConfig visualization2) : base(host,port,device,parameters,updateInterval)
		{
			this.Id = Guid.NewGuid ();
			this.Visualization1 = visualization1;
			this.Visualization2 = visualization2;
		}

	}
}

