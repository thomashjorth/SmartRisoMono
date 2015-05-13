﻿namespace DataModel.ConfigurationModel
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
		









}

