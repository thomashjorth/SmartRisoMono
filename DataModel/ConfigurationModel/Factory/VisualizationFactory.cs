	namespace DataModel.ConfigurationModel.Factory
{
	public enum RealtimeData{
		ActivePower, Frequency, ReactivePower, Temperature, InterphaseVoltages, 
		WindspeedOutsideNacelle, RotorRPM, GeneratorRPM, SOC, EngineRPM, BusbarFrequency, BusbarInterphaseVoltages,

		ActiveEnergyImport, ActiveEnergyExport,
		ACActivePower, ACFrequency, ACReactivePower,

		// Flexhouse
		ActivePowerPhaseA,
		ActivePowerPhaseB,
		ActivePowerPhaseC,
		ReactivePowerPhaseA,
		ReactivePowerPhaseB,
		ReactivePowerPhaseC,
		ReactiveEnergyImport,
		OutsideTemperature,
		OutsideWindspeed,
		OutsideWindDirection,
		SolarIrradiation


	}

	public enum ApplianceData{
		Count , PowerCentroid , EnergyCentroid , Discovered, AEC, Score
	}

	public enum RealtimeInterface{
		GenericLoadWS, GaiaWindTurbineWS, LithiumBatteryWS, FlexHouseWS, VRBBatteryWS, PVSystemWS, DEIFDieselGensetWS
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

