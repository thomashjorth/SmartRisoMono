using System;
using DataModel.ConfigurationModel.Classes;
namespace DataModel.ConfigurationModel
{

	public class MAggregationVisualizationFactory: AbstractMultiAggregationVisualizationFactory
	{

		public MAggregationVisualizationFactory(string dataAggregatorHost, int dataAggregatorPort){
			Host = dataAggregatorHost;
			Port = dataAggregatorPort;

		}
		public override VisualizationConfig CreateBar(MultiAggregation data,int updateInterval,string titleHeading,int valueMin,int valueMax,string unit)
		{
			Parameters = "Get/"+data.ToString();
			return new BarConfig (Host, Port, Device, Parameters, updateInterval, titleHeading, valueMin, valueMax, unit);
		}

		public override VisualizationConfig CreatePie(MultiAggregation data,int updateInterval,string titleHeading)
		{
			Parameters = "Get/"+data.ToString();
			return new PieConfig (Host, Port, Device, Parameters, updateInterval, titleHeading);
		}

	}

}
