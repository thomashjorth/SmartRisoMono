using System;

namespace DataModel.ConfigurationModel.Classes
{
	public class MultiGraphConfig : GraphConfig
	{
		public MultiGraphConfig ( string host, int port, string device, string parameters, int updateIterval, 
			string titleHeading, int yMin, int yMax, int xLength, string unit)
			: base(host, port, device, parameters, updateIterval,titleHeading,yMin,yMax,xLength,unit)
		{
			VisualizationType = "d3MultiGraph";
		}
	}
}

