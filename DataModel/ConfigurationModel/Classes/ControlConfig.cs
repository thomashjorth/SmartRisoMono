using System;
using DataAggregator.Models;

namespace DataAggregator.Models.Configuration
{
	public class ControlConfig : VisualizationConfig
	{
		
		public ControlConfig ( string host, int port, string device, string parameters) 
			: base(host, port, device, parameters, 0)
		{
			VisualizationType = "basicControl";
		}
	}
}

