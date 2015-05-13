using System;
using DataModel.ConfigurationModel.Classes;
namespace DataModel.ConfigurationModel
{

	public abstract class AbstractApplianceVisualizationFactory  
	{
		public string Host;
		public int Port;
		public string Device = "Appliance";

		public abstract VisualizationConfig CreateBar(ApplianceData data,int updateInterval,string titleHeading,int valueMin,int valueMax,string unit);
		public abstract VisualizationConfig CreatePie(ApplianceData data, int updateInterval,string titleHeading);
		public abstract VisualizationConfig CreateTable(ApplianceData data, int updateInterval,string titleHeading);
	}

}
