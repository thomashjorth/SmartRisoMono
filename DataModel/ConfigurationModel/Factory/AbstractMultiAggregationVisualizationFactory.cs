using System;
using DataModel.ConfigurationModel.Classes;
using System.Collections.Generic;


namespace DataModel.ConfigurationModel.Factory
{

	public abstract class AbstractMultiAggregationVisualizationFactory : AbstractAggregationVisualizationFactory
	{

		public abstract VisualizationConfig CreateMultiGraph(List<string> hosts, List<int> ports, RealtimeInterface deviceInterface, RealtimeData data, int updateInterval, string titleHeading, int yMin, int yMax, int xLength, string unit);
		public abstract VisualizationConfig CreateBar(List<string> hosts, List<int> ports, RealtimeInterface deviceInterface, RealtimeData data,int updateInterval,string titleHeading,int valueMin,int valueMax,string unit);
		public abstract VisualizationConfig CreatePie(List<string> hosts, List<int> ports, RealtimeInterface deviceInterface, RealtimeData data,int updateInterval,string titleHeading);
	
	}

}
