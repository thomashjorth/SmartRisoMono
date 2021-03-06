using System;
using DataModel.ConfigurationModel.Classes;
namespace DataModel.ConfigurationModel.Factory
{

	public class ApplianceVisualizationFactory: AbstractApplianceVisualizationFactory
	{
		private string Parameters(ApplianceData data){
			string method = "Get/";
			switch (data) {
			case ApplianceData.Count:
				return method+"?id=Program&attribute=Count";
	
			case ApplianceData.Discovered:
				return  method+"?id=Program&attribute=Discovered";
		
			case ApplianceData.EnergyCentroid:
				return  method+"?id=Program&attribute=EnergyCentroid";
		
			case ApplianceData.PowerCentroid:
				return  method+"?id=Program&attribute=PowerCentroid";
			
			case ApplianceData.Score:
				return  method+"?id=Score&attribute=ALL";
			
			case ApplianceData.AEC:
				return  method+"?id=AEC&attribute=ALL";
				default:
			return "NAN";
			}
		} 
		public ApplianceVisualizationFactory(string dataAggregatorHost, int dataAggregatorPort){
			Host = dataAggregatorHost;
			Port = dataAggregatorPort;
			 
		}
		public override VisualizationConfig CreateBar(ApplianceData data,int updateInterval,string titleHeading,int valueMin,int valueMax,string unit)
		{
			return new BarConfig (Host, Port, Device, Parameters (data), updateInterval, titleHeading, valueMin, valueMax, unit);
		}

		public override VisualizationConfig CreatePie(ApplianceData data,int updateInterval,string titleHeading)
		{
			return new PieConfig (Host, Port, Device, Parameters (data), updateInterval, titleHeading);
		}

		public override VisualizationConfig CreateTable(ApplianceData data,int updateInterval,string titleHeading)
		{
			return new TableConfig (Host, Port, Device, Parameters (data), updateInterval, titleHeading);
		}
		// Todo implement rest of Create functions.

	}
	
}
