using System;
using DataModel.ConfigurationModel.Classes;
using System.Collections.Generic;


namespace DataModel.ConfigurationModel.Factory
{

	public class MAggregationVisualizationFactory: AbstractMultiAggregationVisualizationFactory
	{

		public MAggregationVisualizationFactory(string dataAggregatorHost, int dataAggregatorPort){
			Host = dataAggregatorHost;
			Port = dataAggregatorPort;

		}
		public override VisualizationConfig CreateMultiGraph(List<string> hosts, List<int> ports, RealtimeInterface deviceInterface, RealtimeData data, int updateInterval,string titleHeading,int yMin, int yMax, int xLength, string unit)
		{
			Parameters = "?hosts=";
			for(int i = 0; i<hosts.Count; i++) {
				Parameters += hosts[i] + ":" + ports[i] +";";
			}
			Parameters += "?";
			Parameters = Parameters.Replace(";?","");
			var p = "GetUnits/"+Parameters+ "&wsInterface="+deviceInterface+"&resource=get"+data;
			return new MultiGraphConfig (Host, Port, Device, p, updateInterval, titleHeading, yMin, yMax, xLength*hosts.Count, unit);
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
