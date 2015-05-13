using System;
using DataModel.ConfigurationModel.Classes;
namespace DataModel.ConfigurationModel
{

	public abstract class AbstractRealtimeVisualizationFactory 
	{
		public string Host;
		public int Port;
		public string Device = "Realtime";
		public string Parameters;

		public abstract VisualizationConfig CreateUnit (int updateInterval);
		public abstract VisualizationConfig CreateGraph(RealtimeInterface derInterface, RealtimeData data,int updateInterval,string titleHeading,int yMin, int yMax, int xLength, string unit);
		public abstract VisualizationConfig CreateGauge(RealtimeInterface derInterface, RealtimeData data,int updateInterval,string titleHeading,int valueMin, int valueMax, string unit);
		public abstract VisualizationConfig CreateControl (RealtimeInterface deviceInterface);
	}

}
