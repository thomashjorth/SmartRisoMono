using System;
using DataModel.ConfigurationModel.Classes;
namespace DataModel.ConfigurationModel
{

	public abstract class AbstractMultiAggregationVisualizationFactory : AbstractAggregationVisualizationFactory
	{
		public abstract VisualizationConfig CreateBar(MultiAggregation data,int updateInterval,string titleHeading,int valueMin,int valueMax,string unit);
		public abstract VisualizationConfig CreatePie(MultiAggregation data,int updateInterval,string titleHeading);
	
	}

}
