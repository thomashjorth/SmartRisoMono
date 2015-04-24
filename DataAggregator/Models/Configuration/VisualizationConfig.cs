using System;

namespace DataAggregator
{
	public class VisualizationConfig
	{
		public String Host;
		public int Port;
		public string Device;
		public string Method;
		public string TitleHeading;
		public string VisualizationType;
		public int UpdateInterval;
		public VisualizationConfig (string host,int port,string device,string method,string  titleHeading, int updateInterval)
		{
			
			Host = host;
			Port = port;
			Device = device;
			Method = method;
			TitleHeading = titleHeading;
			UpdateInterval = updateInterval;
		}

		public VisualizationConfig (string host,int port,string device,string  method,string  titleHeading)
		{

			Host = host;
			Port = port;
			Device = device;
			Method = method;
			TitleHeading = titleHeading;
		}

		public VisualizationConfig(){
		}
	}

}

