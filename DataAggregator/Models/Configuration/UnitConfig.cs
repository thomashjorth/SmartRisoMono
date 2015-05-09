using System;
using DataAggregator.Models;

namespace DataAggregator.Models.Configuration
{
	public class UnitConfig : VisualizationConfig
	{
		public VisualizationConfig DER;
		public UnitConfig ( string host, int port, int updateIterval, VisualizationConfig der) : base(host, port, updateIterval)
		{
			VisualizationType = "unit";
			DER = der;
			Device = "DER";
			Method = "?host=" + der.Host + "&port=" + der.Port;
		}
	}
}

