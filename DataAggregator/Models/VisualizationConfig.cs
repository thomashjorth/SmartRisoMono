using System;

namespace DataAggregator
{
	public abstract class VisualizationConfig
	{
		public string VisualizationType;
		public string Host;
		public int Port;
		public string Aggregation;
		public string Resource;
		public int XHeight = 1;
		public int YHeight = 1;

		protected  VisualizationConfig(string host, int port, string aggregation, string resource){
			Host = host;
			Port = port;
			Aggregation = aggregation;
			Resource = resource;
		}
	}
}

