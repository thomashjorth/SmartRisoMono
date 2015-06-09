using System;

namespace DataModel.ConfigurationModel.Classes
{
	public class ExperimentConfig : VisualizationConfig
	{
		public string Image;

		public VisualizationConfig Visualization1;
		public VisualizationConfig Visualization2;

		public ExperimentConfig (string host, int port, string device, 
		                         string parameters, int updateInterval, string image, VisualizationConfig visualization1, 
		                         VisualizationConfig visualization2) : base(host,port,device,parameters,updateInterval)
		{
			this.Image = image;
			this.Visualization1 = visualization1;
			this.Visualization2 = visualization2;
		}

	}
}

