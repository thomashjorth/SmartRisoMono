using System;

namespace DataModel.ConfigurationModel
{
	public class PieConfig : VisualizationConfig
	{
		public string TitleHeading;
		public PieConfig ( string host, int port, string device, string parameters, int updateIterval, string titleHeading) : base(host, port, device, parameters, updateIterval)
		{
			VisualizationType = "d3Pie";
			TitleHeading = titleHeading;
		}
	}
}

