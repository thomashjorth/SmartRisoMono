using System;

namespace DataAggregator
{
	public class VisualizationConfig
	{
		public String Host;
		public int Port;
		public string Aggregation;
		public string Resource;
		public string TitleHeading;
		public string VisualizationType;

		public VisualizationConfig (string host,int port,string aggregation,string  resource,string  titleHeading)
		{
			
			Host = host;
			Port = port;
			Aggregation = aggregation;
			Resource = resource;
			TitleHeading = titleHeading;
		}

		public VisualizationConfig(){
		}
	}

}

