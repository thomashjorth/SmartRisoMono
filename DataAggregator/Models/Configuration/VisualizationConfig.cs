using System;

namespace DataAggregator.Models.Configuration
{
	public class VisualizationConfig
	{

		public string VisualizationType;
		public String Host;
		public int Port;
		public string Device;
		public string Resources;
		public string DER;
		public string Method;
		public string TitleHeading;
		public int UpdateInterval;
		public VisualizationConfig (string host,int port,string device,string method,string  titleHeading, int updateInterval)
		{
			
			Host = host;
			Port = port;
			Device = device;
			Resources = method;
			Method = method;
			DER = method;
			TitleHeading = titleHeading;
			UpdateInterval = updateInterval;
		}

		public VisualizationConfig (string host,int port,string device,string  method,string  titleHeading)
		{

			Host = host;
			Port = port;
			Device = device;
			Resources = method;
			Method = method;
			DER = method;
			TitleHeading = titleHeading;
		}

		public VisualizationConfig(){
		}

		public VisualizationConfig(string host, int port){
			Host = host;
			Port = port;
		}

		public VisualizationConfig(string host, int port, int updateInterval){
			Host = host;
			Port = port;
			UpdateInterval = updateInterval;
		}
	}

}

