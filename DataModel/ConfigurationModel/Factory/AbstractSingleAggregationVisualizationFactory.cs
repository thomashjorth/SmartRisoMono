using System;
using DataModel.ConfigurationModel.Classes;
namespace DataModel.ConfigurationModel.Factory
{

	public abstract class AbstractSingleAggregationVisualizationFactory : AbstractAggregationVisualizationFactory
	{
		public abstract VisualizationConfig CreateGraph(SingleAggregation data,int updateInterval,string titleHeading,int yMin, int yMax, int xLength, string unit);
			
	}

}
