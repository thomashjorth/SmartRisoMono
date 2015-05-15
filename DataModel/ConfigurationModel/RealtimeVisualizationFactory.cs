using System;
using DataModel.ConfigurationModel.Classes;
namespace DataModel.ConfigurationModel
{

	public class RealtimeVisualizationFactory: AbstractRealtimeVisualizationFactory
	{
		public RealtimeVisualizationFactory(string dataAggregatorHost, int dataAggregatorPort, string deviceHost, int devicePort)
		{
			Host = dataAggregatorHost;
			Port = dataAggregatorPort;
			Parameters = "?host=" + deviceHost + "&port=" + devicePort; 

		}

		public override VisualizationConfig CreateUnit(int updateInterval)
		{
			return new UnitConfig (Host, Port, Parameters,updateInterval);
		}
		public override VisualizationConfig CreateGraph(RealtimeInterface deviceInterface, RealtimeData data, int updateInterval,string titleHeading,int yMin, int yMax, int xLength, string unit)
		{
			Parameters+= "&wsInterface="+deviceInterface+"&resource=get"+data;
			return new GraphConfig (Host, Port, Device, Parameters, updateInterval, titleHeading, yMin, yMax, xLength, unit);
		}
		public override VisualizationConfig CreateGauge(RealtimeInterface deviceInterface, RealtimeData data, int updateInterval,string titleHeading,int valueMin, int valueMax, string unit)
		{
			string ID = "g"+ Guid.NewGuid().ToString();
			Parameters+= "&wsInterface="+deviceInterface+"&resource=get"+data;
			return new GaugesConfig(new GaugeConfig(Host,Port,Device,Parameters,updateInterval,titleHeading,valueMin,valueMax,ID,unit));
		}

		public override VisualizationConfig CreateControl(RealtimeInterface deviceInterface)
		{
			Parameters+= "&wsInterface="+deviceInterface;
			return new ControlConfig(Host,Port,Device,Parameters);
		}

	}

}
