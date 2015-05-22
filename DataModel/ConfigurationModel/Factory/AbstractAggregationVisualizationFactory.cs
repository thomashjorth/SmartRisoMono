using System;
using DataModel.ConfigurationModel.Classes;
namespace DataModel.ConfigurationModel.Factory
{

	public abstract class AbstractAggregationVisualizationFactory 
	{
		public string Host;
		public int Port;
		public string Device = "Aggregation";
		public string Parameters;
	}

}
