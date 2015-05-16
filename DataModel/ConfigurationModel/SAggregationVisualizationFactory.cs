using System;
using DataModel.ConfigurationModel.Classes;
namespace DataModel.ConfigurationModel
{

	public class SAggregationVisualizationFactory: AbstractSingleAggregationVisualizationFactory
	{
		public SAggregationVisualizationFactory(string dataAggregatorHost, int dataAggregatorPort){
			Host = dataAggregatorHost;
			Port = dataAggregatorPort;

		}

		public override VisualizationConfig CreateGraph( SingleAggregation data,int updateInterval,string titleHeading,int yMin, int yMax, int xLength, string unit)
		{
			Parameters = "Get/"+data.ToString();
			return new GraphConfig (Host, Port, Device, Parameters, updateInterval, titleHeading, yMin, yMax, xLength, unit);
		}
		// Todo implement rest of Create functions.

	}
}
