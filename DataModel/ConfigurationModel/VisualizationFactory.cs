using System;
using DataModel.ConfigurationModel.Classes;
namespace DataModel.ConfigurationModel
{
	public enum RealtimeData{
		ActivePower
	}

	public enum ApplianceData{
		Count , PowerCentroid , EnergyCentroid , Discovered, AEC, Score
	}

	public enum RealtimeInterface{
		GenericLoadWS
	}

	public enum SingleAggregation{
		AvgActivePower
	}
	public enum MultiAggregation{
		AllActivePower
	}



	public class VisualizationFactory 
	{
		
		public SAggregationVisualizationFactory CreateSingleAggregationFactory(string dataAggregatorHost, int dataAggregatorPort)
		{
			return new SAggregationVisualizationFactory(dataAggregatorHost,dataAggregatorPort);
		} 
		public MAggregationVisualizationFactory CreateMultiAggregationFactory(string dataAggregatorHost, int dataAggregatorPort)
		{
			return new MAggregationVisualizationFactory(dataAggregatorHost,dataAggregatorPort);
		}

		public RealtimeVisualizationFactory CreateRealtimeVizualizationFactory(string dataAggregatorHost, int dataAggregatorPort,string deviceHost, int devicePort)
		{
			return new RealtimeVisualizationFactory( dataAggregatorHost,  dataAggregatorPort,  deviceHost,  devicePort);
		}

		public ApplianceVisualizationFactory CreateApplianceVizualizationFactory(string dataAggregatorHost, int dataAggregatorPort)
		{
			return new ApplianceVisualizationFactory (dataAggregatorHost, dataAggregatorPort);
		}
	}

	public abstract class AbstractAggregationVisualizationFactory 
	{
		public string Host;
		public int Port;
		public string Device = "Aggregation";
		public string Parameters;
	}

	public abstract class AbstractApplianceVisualizationFactory 
	{
		public string Host;
		public int Port;
		public string Device = "Appliance";

		public abstract VisualizationConfig CreateBar(ApplianceData data,int updateInterval,string titleHeading,int valueMin,int valueMax,string unit);
		public abstract VisualizationConfig CreatePie(ApplianceData data, int updateInterval,string titleHeading);
		public abstract VisualizationConfig CreateTable(ApplianceData data, int updateInterval,string titleHeading);
	}

	public abstract class AbstractSingleAggregationVisualizationFactory : AbstractAggregationVisualizationFactory
	{
		public abstract VisualizationConfig CreateGraph(SingleAggregation data,int updateInterval,string titleHeading,int yMin, int yMax, int xLength, string unit);
			
	}

	public abstract class AbstractMultiAggregationVisualizationFactory : AbstractAggregationVisualizationFactory
	{
		public abstract VisualizationConfig CreateBar(MultiAggregation data,int updateInterval,string titleHeading,int valueMin,int valueMax,string unit);
		public abstract VisualizationConfig CreatePie(MultiAggregation data,int updateInterval,string titleHeading);
	
	}

	public abstract class AbstractRealtimeVisualizationFactory
	{
		public string Host;
		public int Port;
		public string Device = "Realtime";
		public string Parameters;

		public abstract VisualizationConfig CreateUnit (int updateInterval);
		public abstract VisualizationConfig CreateGraph(RealtimeInterface derInterface, RealtimeData data,int updateInterval,string titleHeading,int yMin, int yMax, int xLength, string unit);
		public abstract VisualizationConfig CreateGauge(RealtimeInterface derInterface, RealtimeData data,int updateInterval,string titleHeading,int valueMin, int valueMax, string unit);
	}

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
			return new GaugesConfig(new GaugeConfig(Host,Port,Device,Parameters,updateInterval,titleHeading,valueMin,valueMax,ID,unit),null);
		}
	
	}

	public class MAggregationVisualizationFactory: AbstractMultiAggregationVisualizationFactory
	{

		public MAggregationVisualizationFactory(string dataAggregatorHost, int dataAggregatorPort){
			Host = dataAggregatorHost;
			Port = dataAggregatorPort;

		}
		public override VisualizationConfig CreateBar(MultiAggregation data,int updateInterval,string titleHeading,int valueMin,int valueMax,string unit)
		{
			Parameters = data.ToString();
			return new BarConfig (Host, Port, Device, Parameters, updateInterval, titleHeading, valueMin, valueMax, unit);
		}

		public override VisualizationConfig CreatePie(MultiAggregation data,int updateInterval,string titleHeading)
		{
			Parameters = data.ToString();
			return new PieConfig (Host, Port, Device, Parameters, updateInterval, titleHeading);
		}

	}


	public class ApplianceVisualizationFactory: AbstractApplianceVisualizationFactory
	{
		private string Parameters(ApplianceData data){
			switch (data) {
			case ApplianceData.Count:
				return "?id=Program&attribute=Count";
	
			case ApplianceData.Discovered:
				return  "?id=Program&attribute=Discovered";
		
			case ApplianceData.EnergyCentroid:
				return  "?id=Program&attribute=EnergyCentroid";
		
			case ApplianceData.PowerCentroid:
				return  "?id=Program&attribute=PowerCentroid";
			
			case ApplianceData.Score:
				return  "?id=Score&attribute=ALL";
			
			case ApplianceData.AEC:
				return  "?id=AEC&attribute=ALL";
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
	public class SAggregationVisualizationFactory: AbstractSingleAggregationVisualizationFactory
	{
		public SAggregationVisualizationFactory(string dataAggregatorHost, int dataAggregatorPort){
			Host = dataAggregatorHost;
			Port = dataAggregatorPort;

		}

		public override VisualizationConfig CreateGraph( SingleAggregation data,int updateInterval,string titleHeading,int yMin, int yMax, int xLength, string unit)
		{
			Parameters = data.ToString();
			return new GraphConfig (Host, Port, Device, Parameters, updateInterval, titleHeading, yMin, yMax, xLength, unit);
		}
		// Todo implement rest of Create functions.

	}
}

