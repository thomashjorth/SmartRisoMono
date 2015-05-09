using System;

namespace DataAggregator.Models.Configuration
{
	public class VisualizationConfig
	{

		public string VisualizationType;
		public String Host;
		public int Port;
		public string Device;
		public string Params;
		public int UpdateInterval;

		public VisualizationConfig (string host,int port,string device,string parameters, int updateInterval)
		{
			
			Host = host;
			Port = port;
			Device = device;
			Params = parameters;
			UpdateInterval = updateInterval;
		}

		public VisualizationConfig(){
		}
	}

}

