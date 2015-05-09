using System;
using DataAggregator.Models;

namespace DataAggregator.Models.Configuration
{
	public class UnitConfig : VisualizationConfig
	{
		
		public UnitConfig ( string host, int port, string parameters,int updateIterval) 
			: base(host, port, "DER", parameters, updateIterval)
		{
			VisualizationType = "unit";
		}
	}
}

