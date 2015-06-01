using System;
using DataModel.ConfigurationModel.Factory;
using DataModel.ConfigurationModel;
using System.Collections.Generic;
using System.IO;


namespace DataAggregator
{
	public static class ExampleConfigurations
	{
		public static PagesConfig Example1(){

			AbstractPageFactory pageFactory = new EquallySized3x3PageFactory ();

			RealtimeVisualizationFactory realtime = new RealtimeVisualizationFactory ("localhost", 9001, "localhost", 8080);
			RealtimeVisualizationFactory realtime2 = new RealtimeVisualizationFactory ("localhost", 9001, "localhost", 8085);

			MAggregationVisualizationFactory multi = new MAggregationVisualizationFactory ("localhost", 9001);
			SAggregationVisualizationFactory single = new SAggregationVisualizationFactory ("localhost", 9001);

			PagesConfig pages = pageFactory.CreatePages (
				new List<VisualizationConfig>{
					realtime.CreateUnit (2000),realtime2.CreateUnit (2000),
					realtime.CreateGauge(RealtimeInterface.GaiaWindTurbineWS,RealtimeData.ActivePower,2000,"8080 Power",-10,10,"mW",
						new double[,]{{-10,-1}},new double[,]{{-1.0,1}},new double[,]{{1.0,10}}),
					//multi.CreateMultiGraph(new List<string>{"localhost","localhost"},new List<int>{8080,8085},
					//	RealtimeInterface.GaiaWindTurbineWS,RealtimeData.ActivePower,
					//	2000,"8080 and 8085 Power",-2,2,10,"mW"),
					single.CreateGraph(SingleAggregation.AvgActivePower,2000,"Realtime Avg Power",-2,2,10,"mW"),
					multi.CreateBar(MultiAggregation.AllActivePower,3000,"Power",-2,2,"mW"),
					multi.CreatePie(MultiAggregation.AllActivePower,3000,"Power")

				}
			);

			return pages;
		}

		public static PagesConfig WashingMachineExperiment(){
			
			VisualizationFactory VisFac = new VisualizationFactory ();
			AbstractRealtimeVisualizationFactory realtime8080 = 
				VisFac.CreateRealtimeVizualizationFactory ("localhost", 9001, "localhost", 8080);
			AbstractApplianceVisualizationFactory appliance = 
				VisFac.CreateApplianceVizualizationFactory ("127.0.0.1", 9001);

			AbstractPageFactory pageFactory = new EquallySized3x3PageFactory ();

			PagesConfig washingExample = pageFactory.CreatePages (new List<VisualizationConfig> (){ 

				appliance.CreatePie(ApplianceData.Count,2000,"Count"),
				appliance.CreatePie(ApplianceData.PowerCentroid,2000,"Power"),
				appliance.CreatePie(ApplianceData.EnergyCentroid,2000,"Energy"),
				appliance.CreateBar(ApplianceData.Count,2000,"Count",0,10,"#"),
				appliance.CreateBar(ApplianceData.AEC,2000,"AEC",0,400,"kWh"),
				appliance.CreateBar(ApplianceData.Score, 2000,"Score",0,100,""),
				realtime8080.CreateGauge(RealtimeInterface.GenericLoadWS,RealtimeData.ActivePower,2000,"Power",-5,5,"mW",
					new double[,]{{-5.0,1}},
					new double[,]{{1.0,2.0}},new double[,]{{2.0,5.0}}),

				realtime8080.CreateGraph(RealtimeInterface.GenericLoadWS, RealtimeData.ActivePower,2000,"Active Power",-5,5,7200,"mW"),
				appliance.CreateTable(ApplianceData.Discovered, 2000,"Discovered Programs")
			});
			return washingExample;
		}

		public static PagesConfig testGaia(){
			VisualizationFactory VisFac = new VisualizationFactory ();
			AbstractPageFactory pageFactory = new EquallySized3x3PageFactory ();
			AbstractRealtimeVisualizationFactory realtime8080  = 
				VisFac.CreateRealtimeVizualizationFactory ("localhost", 9001, "localhost", 8080);
			PagesConfig gaia= pageFactory.CreatePages(new List<VisualizationConfig> (){ 
				realtime8080.CreateGauge(RealtimeInterface.GaiaWindTurbineWS, RealtimeData.InterphaseVoltages,1000,"Interphase Voltages",0,500,"U [V]",
					new double[,]{{350,450}},null,new double[,]{{0,350},{450,500}}),
				realtime8080.CreateGauge(RealtimeInterface.GaiaWindTurbineWS, RealtimeData.ActivePower,1000,"Active Power",-5,20,"P [kW]",
					new double[,]{{0,11}},new double[,]{{-5,0},{11,15}},new double[,]{{15,20}}),
				realtime8080.CreateGauge(RealtimeInterface.GaiaWindTurbineWS, RealtimeData.WindspeedOutsideNacelle,1000,"Windspeed",0,40,"v [m/s]",
					new double[,]{{0,25}},new double[,]{{25,39}},new double[,]{{39,40}}),
				realtime8080.CreateGauge(RealtimeInterface.GaiaWindTurbineWS, RealtimeData.RotorRPM,1000,"Rotor RPM",0,100,"RPM0 [1/min]",
					new double[,]{{0,62}},new double[,]{{62,70}},new double[,]{{70,100}}),
				realtime8080.CreateGauge(RealtimeInterface.GaiaWindTurbineWS, RealtimeData.GeneratorRPM,1000,"Generator RPM",0,1200,"RPM1 [1/min]",
					new double[,]{{0,1040}},new double[,]{{1040,1100}},new double[,]{{1100,1200}}),
				realtime8080.CreateGraph(RealtimeInterface.GaiaWindTurbineWS, RealtimeData.InterphaseVoltages,1000,"Interphase Voltages",0,500,12,"U [V]"),
				realtime8080.CreateControl(RealtimeInterface.GaiaWindTurbineWS,"gaiaControl"),
				realtime8080.CreateUnit(30000)
			});
			string serializedGaiaConf = Newtonsoft.Json.JsonConvert.SerializeObject (gaia);
			File.Delete ("PageConfigurations/gaia.json");
			File.AppendAllText ("PageConfigurations/gaia.json", serializedGaiaConf);
			return gaia;
		}

		public static PagesConfig testMultiGaiaWashing(){
			VisualizationFactory VisFac = new VisualizationFactory ();
			AbstractPageFactory pageFactory = new EquallySized3x3PageFactory ();
			AbstractRealtimeVisualizationFactory realtime8080  = 
				VisFac.CreateRealtimeVizualizationFactory ("192.168.1.4", 9001, "localhost", 8080);
			AbstractApplianceVisualizationFactory appliance = 
				VisFac.CreateApplianceVizualizationFactory ("192.168.1.4", 9001);
			PagesConfig gaia= pageFactory.CreatePages(new List<VisualizationConfig> (){ 
				appliance.CreatePie(ApplianceData.Count,2000,"Count"),
				appliance.CreatePie(ApplianceData.PowerCentroid,2000,"Power"),
				appliance.CreatePie(ApplianceData.EnergyCentroid,2000,"Energy"),
				appliance.CreateBar(ApplianceData.Count,2000,"Count",0,10,"#"),
				appliance.CreateBar(ApplianceData.AEC,2000,"AEC",0,400,"kWh"),
				appliance.CreateBar(ApplianceData.Score, 2000,"Score",0,100,""),
				realtime8080.CreateGauge(RealtimeInterface.GenericLoadWS,RealtimeData.ActivePower,2000,"Power",-5,5,"mW",
					new double[,]{{-5.0,1}},
					new double[,]{{1.0,2.0}},new double[,]{{2.0,5.0}}),

				realtime8080.CreateGraph(RealtimeInterface.GenericLoadWS, RealtimeData.ActivePower,2000,"Active Power",-5,5,7200,"mW"),
				appliance.CreateTable(ApplianceData.Discovered, 2000,"Discovered Programs"),
				realtime8080.CreateGauge(RealtimeInterface.GaiaWindTurbineWS, RealtimeData.InterphaseVoltages,1000,"Interphase Voltages",0,500,"U [V]",
					new double[,]{{350,450}},null,new double[,]{{0,350},{450,500}}),
				realtime8080.CreateGauge(RealtimeInterface.GaiaWindTurbineWS, RealtimeData.ActivePower,1000,"Active Power",-5,20,"P [kW]",
					new double[,]{{0,11}},new double[,]{{-5,0},{11,15}},new double[,]{{15,20}}),
				realtime8080.CreateGauge(RealtimeInterface.GaiaWindTurbineWS, RealtimeData.WindspeedOutsideNacelle,1000,"Windspeed",0,40,"v [m/s]",
					new double[,]{{0,25}},new double[,]{{25,39}},new double[,]{{39,40}}),
				realtime8080.CreateGauge(RealtimeInterface.GaiaWindTurbineWS, RealtimeData.RotorRPM,1000,"Rotor RPM",0,100,"RPM0 [1/min]",
					new double[,]{{0,62}},new double[,]{{62,70}},new double[,]{{70,100}}),
				realtime8080.CreateGauge(RealtimeInterface.GaiaWindTurbineWS, RealtimeData.GeneratorRPM,1000,"Generator RPM",0,1200,"RPM1 [1/min]",
					new double[,]{{0,1040}},new double[,]{{1040,1100}},new double[,]{{1100,1200}}),
				realtime8080.CreateGraph(RealtimeInterface.GaiaWindTurbineWS, RealtimeData.InterphaseVoltages,1000,"Interphase Voltages",0,500,12,"U [V]"),
				realtime8080.CreateControl(RealtimeInterface.GaiaWindTurbineWS,"gaiaControl"),
				realtime8080.CreateUnit(30000)
			});
			string serializedGaiaConf = Newtonsoft.Json.JsonConvert.SerializeObject (gaia);
			File.Delete ("PageConfigurations/testMultiGaiaWashing.json");
			File.AppendAllText ("PageConfigurations/testMultiGaiaWashing.json", serializedGaiaConf);
			return gaia;
		}

		public static PagesConfig testEmpty(){
			VisualizationFactory VisFac = new VisualizationFactory ();
			AbstractPageFactory pageFactory = new EquallySized3x3PageFactory ();
			PagesConfig gaia= pageFactory.CreatePages(new List<VisualizationConfig> (){ 
				
			});
			return gaia;
		}
	}
}

