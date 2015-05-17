using System;
using DataModel.ConfigurationModel;

namespace DataModel.ConfigurationModel.Classes
{
	public class ControlConfig : VisualizationConfig
	{
		
		public ControlConfig ( string host, int port, string device, string parameters, string visualizationType) 
			: base(host, port, device, parameters, 0)
		{
			VisualizationType = visualizationType;
		}
	}
}

