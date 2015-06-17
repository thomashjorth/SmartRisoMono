using System;
using DataModel.ConfigurationModel.Factory;
using DataModel.ConfigurationModel;
using System.Collections.Generic;
using System.IO;
using DataModel.ConfigurationModel.Pages;
using DataModel.ConfigurationModel.Classes;

namespace DataAggregator
{
	public static class ExampleConfigurations
	{
		public static PagesConfig ExamPresentation(){

			PageFactory pageFactory = new PageFactory ();

			int updateInterval = 2000;

			string aggregatorHost= "192.168.0.74"; int aggregatorPort = 8080;

			string realtimeHost05 = "syslab-05"; int realtimePort05 = 8080; // Dumpload
			string realtimeHost05Simul = "localhost"; int realtimePort05Simul = 8080; // Dumpload
			string realtimeHost03 = "syslab-03"; int realtimePort03 = 8080; // Gaia
			string realtimeHost07 = "syslab-07"; int realtimePort07 = 8080; // PV
			string realtimeHost10 = "syslab-10"; int realtimePort10 = 8080; // PV
			string realtimeHost12 = "syslab-12"; int realtimePort12 = 8080; // VRBBattery
			string realtimeHost16 = "syslab-16"; int realtimePort16 = 8080; // mobileload
			string realtimeHost18 = "syslab-18"; int realtimePort18 = 8080; // mobileload
			string realtimeHost27 = "syslab-27"; int realtimePort27 = 8080; // dump

			string realtimeHost08 = "syslab-08"; int realtimePort08 = 8085; // Flexhouse

			VisualizationFactory VisFac = new VisualizationFactory ();
			AbstractApplianceVisualizationFactory appliance = 
				VisFac.CreateApplianceVizualizationFactory (aggregatorHost, aggregatorPort); // Washing Machine Experiment
			
			RealtimeVisualizationFactory realtime05 = new RealtimeVisualizationFactory (aggregatorHost, aggregatorPort, realtimeHost05, realtimePort05);
			RealtimeVisualizationFactory realtime03 = new RealtimeVisualizationFactory (aggregatorHost, aggregatorPort,realtimeHost03, realtimePort03);
			RealtimeVisualizationFactory realtime07 = new RealtimeVisualizationFactory (aggregatorHost, aggregatorPort, realtimeHost07, realtimePort07);
			RealtimeVisualizationFactory realtime08 = new RealtimeVisualizationFactory (aggregatorHost, aggregatorPort, realtimeHost08, realtimePort08);
			RealtimeVisualizationFactory realtime10 = new RealtimeVisualizationFactory (aggregatorHost, aggregatorPort, realtimeHost10, realtimePort10);
			RealtimeVisualizationFactory realtime12 = new RealtimeVisualizationFactory (aggregatorHost, aggregatorPort, realtimeHost12, realtimePort12);
			RealtimeVisualizationFactory realtime16 = new RealtimeVisualizationFactory (aggregatorHost, aggregatorPort, realtimeHost16, realtimePort16);
			RealtimeVisualizationFactory realtime18 = new RealtimeVisualizationFactory (aggregatorHost, aggregatorPort, realtimeHost18, realtimePort18);
			RealtimeVisualizationFactory realtime27 = new RealtimeVisualizationFactory (aggregatorHost, aggregatorPort, realtimeHost27, realtimePort27);

			RealtimeVisualizationFactory realtime05Simul = new RealtimeVisualizationFactory (aggregatorHost, aggregatorPort, realtimeHost05Simul, realtimePort05Simul);


			MAggregationVisualizationFactory multi = new MAggregationVisualizationFactory (aggregatorHost, aggregatorPort);
			SAggregationVisualizationFactory single = new SAggregationVisualizationFactory (aggregatorHost, aggregatorPort);

			// Page 1 Dumpload
			List<ExperimentConfig> visualizations1 = new List<ExperimentConfig>{
				realtime05.CreateExperiment (
					RealtimeInterface.GenericLoadWS,
					RealtimeData.ActivePower, 
					updateInterval,
					realtime05.CreateGraph (RealtimeInterface.GenericLoadWS, RealtimeData.ActivePower, updateInterval, 
						"ActivePower", -1, 1, 10, "mW"),
					realtime05.CreateGraph(RealtimeInterface.GenericLoadWS,RealtimeData.ReactivePower,updateInterval,
						"ReactivePower",-1,1,10,"mW")
				)
				,
				realtime03.CreateExperiment (
					RealtimeInterface.GaiaWindTurbineWS,
					RealtimeData.ActivePower, 
					updateInterval,
					realtime03.CreateGraph (RealtimeInterface.GaiaWindTurbineWS, RealtimeData.ActivePower, updateInterval, 
						"ActivePower", -5, 15, 10, "mW"),
					realtime03.CreateGraph(RealtimeInterface.GaiaWindTurbineWS,RealtimeData.GeneratorRPM,updateInterval,
						"GeneratorRPM",1000,1050,10,"mW")
				),
				realtime07.CreateExperiment (
					RealtimeInterface.LithiumBatteryWS,
					RealtimeData.SOC, 
					updateInterval,
					realtime07.CreateGraph (RealtimeInterface.LithiumBatteryWS, RealtimeData.SOC, updateInterval, 
						"SOC", 0, 100, 10, "mW"),
					realtime07.CreateGraph(RealtimeInterface.LithiumBatteryWS,RealtimeData.Temperature,updateInterval,
						"Temperature",0,100,10,"mW")
				),
				realtime03.CreateExperiment (
					RealtimeInterface.GaiaWindTurbineWS,
					RealtimeData.ActivePower, 
					updateInterval,
					realtime03.CreateGraph (RealtimeInterface.GaiaWindTurbineWS, RealtimeData.ActivePower, updateInterval, 
						"ActivePower", -5, 15, 10, "mW"),
					realtime03.CreateGraph(RealtimeInterface.GaiaWindTurbineWS,RealtimeData.GeneratorRPM,updateInterval,
						"GeneratorRPM",1000,1050,10,"mW")
				),
				realtime07.CreateExperiment (
					RealtimeInterface.LithiumBatteryWS,
					RealtimeData.SOC, 
					updateInterval,
					realtime07.CreateGraph (RealtimeInterface.LithiumBatteryWS, RealtimeData.SOC, updateInterval, 
						"SOC", 0, 100, 10, "mW"),
					realtime07.CreateGraph(RealtimeInterface.LithiumBatteryWS,RealtimeData.Temperature,updateInterval,
						"Temperature",0,100,10,"mW")
				)
			};
			ExperimentPageConfig page1 = new ExperimentPageConfig (visualizations1, "Experiment",aggregatorHost,aggregatorPort);

			// Page 2 // Washing Machine
			PageConfig page2 = pageFactory.Create3x3Page (new List<VisualizationConfig> (){ 
				appliance.CreateBar(ApplianceData.Count,1000,"Count",0,5,"#"),
				appliance.CreateBar(ApplianceData.AEC,1000,"AEC",0,400,"kWh"),
				appliance.CreateBar(ApplianceData.Score, 1000,"Score",0,100,""),
				appliance.CreatePie(ApplianceData.Count,1000,"Count"),
				appliance.CreatePie(ApplianceData.EnergyCentroid,1000,"Energy"),
				appliance.CreateTable(ApplianceData.Discovered, 1000,"Discovered Programs"),
			},"Grid3x3");

			// Page 3
			List<VisualizationConfig> visualizations3 = new List<VisualizationConfig>{
				realtime10.CreateGraph(RealtimeInterface.PVSystemWS,RealtimeData.ActivePower,updateInterval,"PV-10 ActivePower",-1000,2000,10,"W"),
				realtime10.CreateGraph(RealtimeInterface.PVSystemWS,RealtimeData.ACFrequency,updateInterval,"PV-10 ACFrequency",-1000,2000,10,"W"),
				realtime10.CreateGraph(RealtimeInterface.PVSystemWS,RealtimeData.ACReactivePower,updateInterval,"PV-10 ACReactivePower",-1000,2000,10,"W"),
				realtime10.CreateGraph(RealtimeInterface.PVSystemWS,RealtimeData.ReactiveEnergyImport,updateInterval,"PV-10 ReactiveEnergyImport",-1000,2000,10,"W"),
				realtime10.CreateGraph(RealtimeInterface.PVSystemWS,RealtimeData.ActiveEnergyExport,updateInterval,"PV-10 ActiveEnergyExport",-1000,2000,10,"W"),
				realtime10.CreateGraph(RealtimeInterface.PVSystemWS,RealtimeData.ActiveEnergyImport,updateInterval,"PV-10 ActiveEnergyImport",-1000,2000,10,"W"),
			};
			PageConfig page3 = pageFactory.Create3x3Page (visualizations3,"Grid3x3");
			// Page 4
			List<VisualizationConfig> visualizations4 = new List<VisualizationConfig>{
				realtime07.CreateGraph(RealtimeInterface.PVSystemWS,RealtimeData.ActivePower,updateInterval,"PV-07 ActivePower",-1000,2000,10,"W"),
				realtime07.CreateGraph(RealtimeInterface.PVSystemWS,RealtimeData.ACFrequency,updateInterval,"PV-07 ACFrequency",-1000,2000,10,"W"),
				realtime07.CreateGraph(RealtimeInterface.PVSystemWS,RealtimeData.ACReactivePower,updateInterval,"PV-07 ACReactivePower",-1000,2000,10,"W"),
				realtime07.CreateGraph(RealtimeInterface.PVSystemWS,RealtimeData.ReactiveEnergyImport,updateInterval,"PV-07 ReactiveEnergyImport",-1000,2000,10,"W"),
				realtime07.CreateGraph(RealtimeInterface.PVSystemWS,RealtimeData.ActiveEnergyExport,updateInterval,"PV-07 ActiveEnergyExport",-1000,2000,10,"W"),
				realtime07.CreateGraph(RealtimeInterface.PVSystemWS,RealtimeData.ActiveEnergyImport,updateInterval,"PV-07 ActiveEnergyImport",-1000,2000,10,"W"),
			};
			PageConfig page4 = pageFactory.Create3x3Page (visualizations4,"Grid3x3");

			// Page 5
			List<VisualizationConfig> visualizations5 = new List<VisualizationConfig>{
				realtime12.CreateGraph(RealtimeInterface.VRBBatteryWS,RealtimeData.ActivePower,updateInterval,"Battery-12 ActivePower",-1000,2000,10,"W"),
				realtime12.CreateGraph(RealtimeInterface.VRBBatteryWS,RealtimeData.ACFrequency,updateInterval,"Battery-12 ACFrequency",-1000,2000,10,"W"),
				realtime12.CreateGraph(RealtimeInterface.VRBBatteryWS,RealtimeData.ACReactivePower,updateInterval,"Battery-12 ACReactivePower",-1000,2000,10,"W"),
				realtime12.CreateGraph(RealtimeInterface.VRBBatteryWS,RealtimeData.ReactiveEnergyImport,updateInterval,"Battery-12 ReactiveEnergyImport",-1000,2000,10,"W"),
				realtime12.CreateGraph(RealtimeInterface.VRBBatteryWS,RealtimeData.ActiveEnergyExport,updateInterval,"Battery-12 ActiveEnergyExport",-1000,2000,10,"W"),
				realtime12.CreateGraph(RealtimeInterface.VRBBatteryWS,RealtimeData.ActiveEnergyImport,updateInterval,"Battery-12 ActiveEnergyImport",-1000,2000,10,"W"),
			};
			PageConfig page5 = pageFactory.Create3x3Page (visualizations5,"Grid3x3");

			// Page 6 Flexhouse
			List<VisualizationConfig> visualizations6 = new List<VisualizationConfig>{

				realtime08.CreateGauge(RealtimeInterface.FlexHouseWS,RealtimeData.ActivePowerPhaseA,updateInterval,
					"Flexhouse-08 ActivePowerPhaseA",-2000,2000,"W",
					new double[,]{{-2000,0}},new double[,]{{0,1000}},new double[,]{{1000,2000}}),
				realtime08.CreateGauge(RealtimeInterface.FlexHouseWS,RealtimeData.ActivePowerPhaseB,updateInterval,
					"Flexhouse-08 ActivePowerPhaseB",-2000,2000,"W",
					new double[,]{{-2000,0}},new double[,]{{0,1000}},new double[,]{{1000,2000}}),
				realtime08.CreateGauge(RealtimeInterface.FlexHouseWS,RealtimeData.ActivePowerPhaseC,updateInterval,
					"Flexhouse-08 ActivePowerPhaseC",-2000,2000,"W",
					new double[,]{{-2000,0}},new double[,]{{0,1000}},new double[,]{{1000,2000}}),
				realtime08.CreateGauge(RealtimeInterface.FlexHouseWS,RealtimeData.ReactivePowerPhaseA,updateInterval,
					"Flexhouse-08 ReactivePowerPhaseA",-2000,2000,"W",
					new double[,]{{-2000,0}},new double[,]{{0,1000}},new double[,]{{1000,2000}}),
				realtime08.CreateGauge(RealtimeInterface.FlexHouseWS,RealtimeData.ReactivePowerPhaseB,updateInterval,
					"Flexhouse-08 ReactivePowerPhaseB",-2000,2000,"W",
					new double[,]{{-2000,0}},new double[,]{{0,1000}},new double[,]{{1000,2000}}),
				realtime08.CreateGauge(RealtimeInterface.FlexHouseWS,RealtimeData.ReactivePowerPhaseC,updateInterval,
					"Flexhouse-08 ReactivePowerPhaseC",-2000,2000,"W",
					new double[,]{{-2000,0}},new double[,]{{0,1000}},new double[,]{{1000,2000}}),
				realtime08.CreateGauge(RealtimeInterface.FlexHouseWS,RealtimeData.ReactiveEnergyImport,updateInterval,
					"Flexhouse-08 ReactiveEnergyImport",-2000,2000,"W",
					new double[,]{{-2000,0}},new double[,]{{0,1000}},new double[,]{{1000,2000}}),
				realtime08.CreateGauge(RealtimeInterface.FlexHouseWS,RealtimeData.ActiveEnergyImport,updateInterval,
					"Flexhouse-08 ActiveEnergyImport",-2000,2000,"W",
					new double[,]{{-2000,0}},new double[,]{{0,1000}},new double[,]{{1000,2000}}),
				realtime08.CreateGauge(RealtimeInterface.FlexHouseWS,RealtimeData.ActiveEnergyExport,updateInterval,
					"Flexhouse-08 ActiveEnergyExport",-2000,2000,"W",
					new double[,]{{-2000,0}},new double[,]{{0,1000}},new double[,]{{1000,2000}}),
			};

			PageConfig page6 = new PageConfig (visualizations6,"Grid3x3");

			// Page 7 Simul Dumpload
			List<VisualizationConfig> visualizations7 = new List<VisualizationConfig>{

				realtime05Simul.CreateGauge(RealtimeInterface.GenericLoadWS,RealtimeData.ActivePower,updateInterval,
					"Flexhouse-08 ActivePowerPhaseA",-1,2,"W",
					new double[,]{{-1,0}},new double[,]{{0,1}},new double[,]{{1,2}}),
				realtime05Simul.CreateControl(RealtimeInterface.GenericLoadWS,"dumploadControl")
		
			};

			PageConfig page7 = new PageConfig (visualizations7,"Grid3x3");

			// Page 8 Dumpload
			List<ExperimentConfig> visualizations8 = new List<ExperimentConfig>{
				realtime05Simul.CreateExperiment (
					RealtimeInterface.GenericLoadWS,
					RealtimeData.ActivePower, 
					updateInterval,
					realtime05Simul.CreateGraph (RealtimeInterface.GenericLoadWS, RealtimeData.ActivePower, updateInterval, 
						"ActivePower", -1, 1, 10, "mW"),
					realtime05Simul.CreateControl(RealtimeInterface.GenericLoadWS,"dumploadControl")
				)
				,
				realtime03.CreateExperiment (
					RealtimeInterface.GaiaWindTurbineWS,
					RealtimeData.ActivePower, 
					updateInterval,
					realtime03.CreateGraph (RealtimeInterface.GaiaWindTurbineWS, RealtimeData.ActivePower, updateInterval, 
						"ActivePower", -5, 15, 10, "mW"),
					realtime03.CreateGraph(RealtimeInterface.GaiaWindTurbineWS,RealtimeData.GeneratorRPM,updateInterval,
						"GeneratorRPM",1000,1050,10,"mW")
				),
				realtime07.CreateExperiment (
					RealtimeInterface.LithiumBatteryWS,
					RealtimeData.SOC, 
					updateInterval,
					realtime07.CreateGraph (RealtimeInterface.LithiumBatteryWS, RealtimeData.SOC, updateInterval, 
						"SOC", 0, 100, 10, "mW"),
					realtime07.CreateGraph(RealtimeInterface.LithiumBatteryWS,RealtimeData.Temperature,updateInterval,
						"Temperature",0,100,10,"mW")
				),
				realtime03.CreateExperiment (
					RealtimeInterface.GaiaWindTurbineWS,
					RealtimeData.ActivePower, 
					updateInterval,
					realtime03.CreateGraph (RealtimeInterface.GaiaWindTurbineWS, RealtimeData.ActivePower, updateInterval, 
						"ActivePower", -5, 15, 10, "mW"),
					realtime03.CreateGraph(RealtimeInterface.GaiaWindTurbineWS,RealtimeData.GeneratorRPM,updateInterval,
						"GeneratorRPM",1000,1050,10,"mW")
				),
				realtime07.CreateExperiment (
					RealtimeInterface.LithiumBatteryWS,
					RealtimeData.SOC, 
					updateInterval,
					realtime07.CreateGraph (RealtimeInterface.LithiumBatteryWS, RealtimeData.SOC, updateInterval, 
						"SOC", 0, 100, 10, "mW"),
					realtime07.CreateGraph(RealtimeInterface.LithiumBatteryWS,RealtimeData.Temperature,updateInterval,
						"Temperature",0,100,10,"mW")
				)
			};
			ExperimentPageConfig page8 = new ExperimentPageConfig (visualizations8, "Experiment",aggregatorHost,aggregatorPort);



			PagesConfig pages = pageFactory.CreatePages (new List<MasterPageConfig> (){ page1, page2,page3, page4, page5, page6,page7 ,page8});

			string serializedGaiaConf = Newtonsoft.Json.JsonConvert.SerializeObject (pages);
			File.Delete ("PageConfigurations/BigPres.json");
			File.AppendAllText ("PageConfigurations/BigPres.json", serializedGaiaConf);
			return pages;
		}

		public static PagesConfig Example1(){

			PageFactory pageFactory = new PageFactory ();

			RealtimeVisualizationFactory realtime = new RealtimeVisualizationFactory ("localhost", 9001, "localhost", 8080);
			RealtimeVisualizationFactory realtime2 = new RealtimeVisualizationFactory ("localhost", 9001, "localhost", 8085);

			MAggregationVisualizationFactory multi = new MAggregationVisualizationFactory ("localhost", 9001);
			SAggregationVisualizationFactory single = new SAggregationVisualizationFactory ("localhost", 9001);

			PagesConfig pages = pageFactory.Create3x3Pages (
				new List<VisualizationConfig>{
					realtime.CreateUnit (2000),realtime2.CreateUnit (2000),
					realtime.CreateGauge(RealtimeInterface.GaiaWindTurbineWS,RealtimeData.ActivePower,2000,"8080 Power",-10,10,"mW",
						new double[,]{{-10,-1}},new double[,]{{-1.0,1}},new double[,]{{1.0,10}}),
					multi.CreateMultiGraph(new List<string>{"localhost","localhost"},new List<int>{8080,8085},
						RealtimeInterface.GaiaWindTurbineWS,RealtimeData.ActivePower,
						2000,"8080 and 8085 Power",0,12,10,"mW"),
					single.CreateGraph(SingleAggregation.AvgActivePower,2000,"Realtime Avg Power",0,12,10,"mW"),
					multi.CreateMultiGraph(new List<string>{"localhost","localhost","localhost"},new List<int>{8080,8085,8090},
						RealtimeInterface.GaiaWindTurbineWS,RealtimeData.ActivePower,
						2000,"8080 and 8085 Power",0,12,10,"mW"),
					multi.CreateBar(new List<string>{"localhost","localhost"},new List<int>{8080,8085},RealtimeInterface.GaiaWindTurbineWS,RealtimeData.ActivePower,2000,"Active Power",0,12,"mW"),
					multi.CreatePie(new List<string>{"localhost","localhost"},new List<int>{8080,8085},RealtimeInterface.GaiaWindTurbineWS,RealtimeData.ActivePower,2000,"Active Power"),
					multi.CreatePie(new List<string>{"localhost","localhost","localhost"},new List<int>{8080,8085,8090},RealtimeInterface.GaiaWindTurbineWS,RealtimeData.ActivePower,2000,"Active Power"),
					multi.CreateBar(new List<string>{"localhost","localhost","localhost"},new List<int>{8080,8085,8090},RealtimeInterface.GaiaWindTurbineWS,RealtimeData.ActivePower,2000,"Active Power",0,12,"mW"),

			},"Grid3x3"
			);

			return pages;
		}

		public static PagesConfig WashingMachineExperiment(){
			
			VisualizationFactory VisFac = new VisualizationFactory ();
			AbstractRealtimeVisualizationFactory realtime8080 = 
				VisFac.CreateRealtimeVizualizationFactory ("localhost", 9001, "localhost", 8080);
			AbstractApplianceVisualizationFactory appliance = 
				VisFac.CreateApplianceVizualizationFactory ("localhost", 9001);

			PageFactory pageFactory = new PageFactory ();

			PagesConfig washingExample = pageFactory.Create3x3Pages (new List<VisualizationConfig> (){ 

				appliance.CreatePie(ApplianceData.Count,1000,"Count"),
				appliance.CreatePie(ApplianceData.EnergyCentroid,1000,"Energy"),
				appliance.CreateBar(ApplianceData.Count,1000,"Count",0,5,"#"),
				appliance.CreateBar(ApplianceData.AEC,1000,"AEC",0,400,"kWh"),
				appliance.CreateBar(ApplianceData.Score, 1000,"Score",0,100,""),
				realtime8080.CreateGauge(RealtimeInterface.GenericLoadWS,RealtimeData.ActivePower,1000,"Power",0,3000000,"mW",
					new double[,]{{0.0,2000000.0}},
					new double[,]{{0.0,0.0}},new double[,]{{2000000.0,3000000.0}}),

				realtime8080.CreateGraph(RealtimeInterface.GenericLoadWS, RealtimeData.ActivePower,1000,"Active Power",-5,5,7200,"mW"),
				appliance.CreateTable(ApplianceData.Discovered, 1000,"Discovered Programs"),
			},"Grid3x3");
			return washingExample;
		}

		public static PagesConfig testGaia(){
			VisualizationFactory VisFac = new VisualizationFactory ();
			PageFactory pageFactory = new PageFactory ();
			AbstractRealtimeVisualizationFactory realtime8080  = 
				VisFac.CreateRealtimeVizualizationFactory ("localhost", 9001, "localhost", 8080);
			PagesConfig gaia= pageFactory.Create3x3Pages(new List<VisualizationConfig> (){ 
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
			},"Grid3x3");
			string serializedGaiaConf = Newtonsoft.Json.JsonConvert.SerializeObject (gaia);
			File.Delete ("PageConfigurations/gaia.json");
			File.AppendAllText ("PageConfigurations/gaia.json", serializedGaiaConf);
			return gaia;
		}

		public static PagesConfig testMultiGaiaWashing(){
			VisualizationFactory VisFac = new VisualizationFactory ();
			PageFactory pageFactory = new PageFactory ();
			AbstractRealtimeVisualizationFactory realtime8080  = 
				VisFac.CreateRealtimeVizualizationFactory ("192.168.1.4", 9001, "localhost", 8080);
			AbstractApplianceVisualizationFactory appliance = 
				VisFac.CreateApplianceVizualizationFactory ("192.168.1.4", 9001);
			PagesConfig gaia= pageFactory.Create3x3Pages(new List<VisualizationConfig> (){ 
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
			},"Grid3x3");
			string serializedGaiaConf = Newtonsoft.Json.JsonConvert.SerializeObject (gaia);
			File.Delete ("PageConfigurations/testMultiGaiaWashing.json");
			File.AppendAllText ("PageConfigurations/testMultiGaiaWashing.json", serializedGaiaConf);
			return gaia;
		}

		public static PagesConfig testEmpty(){
			VisualizationFactory VisFac = new VisualizationFactory ();
			PageFactory pageFactory = new PageFactory ();
			PagesConfig gaia= pageFactory.Create3x3Pages(new List<VisualizationConfig> (){ 
				
			},"Grid3x3");
			return gaia;
		}

		public static PagesConfig Experiement(){

			PageFactory pageFactory = new PageFactory ();

			RealtimeVisualizationFactory realtime = new RealtimeVisualizationFactory ("127.0.0.1", 9001, "localhost", 8080);
			RealtimeVisualizationFactory realtime2 = new RealtimeVisualizationFactory ("127.0.0.1", 9001, "localhost", 8085);
			RealtimeVisualizationFactory realtime3 = new RealtimeVisualizationFactory ("127.0.0.1", 9001, "localhost", 8090);

			List<ExperimentConfig> experiments = new List<ExperimentConfig>{
				new ExperimentConfig(
					"127.0.0.1",
					8080,
					"GenericLoadWS",
					"getActivePower"
					,5000,
					realtime.CreateGraph(RealtimeInterface.GenericLoadWS,RealtimeData.ActivePower,3000,"Power",-10,10,10,"mW"),
					realtime.CreateGraph(RealtimeInterface.GenericLoadWS,RealtimeData.ReactivePower,3000,"Power",-10,10,10,"mW")
				)
			,
				new ExperimentConfig(
					"127.0.0.1",
					8085,
					"GaiaWindTurbineWS",
					"getActivePower"
					,5000,
					realtime2.CreateGraph(RealtimeInterface.GaiaWindTurbineWS,RealtimeData.RotorRPM,3000,"Power",-10,10,10,"mW"),
					realtime2.CreateGraph(RealtimeInterface.GaiaWindTurbineWS,RealtimeData.ActivePower,3000,"Power",-10,10,10,"mW")
						)
				,
				new ExperimentConfig(
					"127.0.0.1",
					8090,
					"LithiumBatteryWS",
					"getSOC"
					,5000,
					realtime3.CreateGraph(RealtimeInterface.LithiumBatteryWS,RealtimeData.SOC,3000,"Power",-10,10,10,"mW"),
					realtime3.CreateGraph(RealtimeInterface.LithiumBatteryWS,RealtimeData.Temperature,3000,"Power",-10,10,10,"mW")
						)
			};
			ExperimentPageConfig b = new ExperimentPageConfig (experiments, "Experiment","127.0.0.1",9001);


			return pageFactory.CreatePages (new List<MasterPageConfig> (){ b });
		}

		public static PagesConfig ExperiementAndWashingMachine(){

			PageFactory pageFactory = new PageFactory ();

			RealtimeVisualizationFactory realtime = new RealtimeVisualizationFactory ("127.0.0.1", 9001, "127.0.0.1", 8080);
			RealtimeVisualizationFactory realtime2 = new RealtimeVisualizationFactory ("127.0.0.1", 9001, "127.0.0.1", 8085);
			RealtimeVisualizationFactory realtime3 = new RealtimeVisualizationFactory ("127.0.0.1", 9001, "127.0.0.1", 8090);

		

			List<ExperimentConfig> experiments = new List<ExperimentConfig>{
				realtime.CreateExperiment (
					RealtimeInterface.GenericLoadWS,
					RealtimeData.ActivePower, 
					5000,
					realtime.CreateGraph (RealtimeInterface.GenericLoadWS, RealtimeData.ActivePower, 6000, "ActivePower", -1, 1, 10, "mW"),
					realtime.CreateGraph(RealtimeInterface.GenericLoadWS,RealtimeData.ReactivePower,6000,"ReactivePower",-1,1,10,"mW")
				)
				,
				realtime2.CreateExperiment (
					RealtimeInterface.GaiaWindTurbineWS,
					RealtimeData.ActivePower, 
					5000,
					realtime2.CreateGraph (RealtimeInterface.GaiaWindTurbineWS, RealtimeData.ActivePower, 6000, "ActivePower", -5, 15, 10, "mW"),
					realtime2.CreateGraph(RealtimeInterface.GaiaWindTurbineWS,RealtimeData.GeneratorRPM,6000,"GeneratorRPM",1000,1050,10,"mW")
					),
				realtime3.CreateExperiment (
					RealtimeInterface.LithiumBatteryWS,
					RealtimeData.SOC, 
					5000,
					realtime3.CreateGraph (RealtimeInterface.LithiumBatteryWS, RealtimeData.SOC, 6000, "SOC", 0, 100, 10, "mW"),
					realtime3.CreateGraph(RealtimeInterface.LithiumBatteryWS,RealtimeData.Temperature,6000,"Temperature",0,100,10,"mW")
					),
				realtime2.CreateExperiment (
					RealtimeInterface.GaiaWindTurbineWS,
					RealtimeData.ActivePower, 
					5000,
					realtime2.CreateGraph (RealtimeInterface.GaiaWindTurbineWS, RealtimeData.ActivePower, 6000, "ActivePower", -5, 15, 10, "mW"),
					realtime2.CreateGraph(RealtimeInterface.GaiaWindTurbineWS,RealtimeData.GeneratorRPM,6000,"GeneratorRPM",1000,1050,10,"mW")
					),
				realtime3.CreateExperiment (
					RealtimeInterface.LithiumBatteryWS,
					RealtimeData.SOC, 
					5000,
					realtime3.CreateGraph (RealtimeInterface.LithiumBatteryWS, RealtimeData.SOC, 6000, "SOC", 0, 100, 10, "mW"),
					realtime3.CreateGraph(RealtimeInterface.LithiumBatteryWS,RealtimeData.Temperature,6000,"Temperature",0,100,10,"mW")
					)
			};
			ExperimentPageConfig b = new ExperimentPageConfig (experiments, "Experiment","127.0.0.1",9001);

			PagesConfig c = WashingMachineExperiment ();

			return pageFactory.CreatePages (new List<MasterPageConfig> (){ b, c.Pages[0] });
		}
		/*
		public static PagesConfig BatteryPage(){

			PageFactory pageFactory = new PageFactory ();

			RealtimeVisualizationFactory realtime3 = new RealtimeVisualizationFactory ("127.0.0.1", 9001, "127.0.0.1", 8090);
			MAggregationVisualizationFactory multi = new MAggregationVisualizationFactory ("127.0.0.1", 9001);


			List<VisualizationConfig> visualizations = new List<VisualizationConfig>{
				realtime3.CreateUnit(3000),
				realtime3.CreateGraph(RealtimeInterface.LithiumBatteryWS,RealtimeData.ActiveEnergyExport,3000,"Export",0,10,10,"W"),
				realtime3.CreateGraph(RealtimeInterface.LithiumBatteryWS,RealtimeData.ActiveEnergyImport,3000,"Import",0,10,10,"W"),

				realtime3.CreateGraph(RealtimeInterface.LithiumBatteryWS,RealtimeData.ACReactivePower,3000,"AC Reactive Power",0,10,10,"W"),
				realtime3.CreateGraph(RealtimeInterface.LithiumBatteryWS,RealtimeData.ACFrequency,3000,"AC Frequency",0,10,10,"W"),
				realtime3.CreateGraph(RealtimeInterface.LithiumBatteryWS,RealtimeData.ACActivePower,3000,"AC Active Power",0,10,10,"W"),

				realtime3.CreateControl(RealtimeInterface.LithiumBatteryWS,"Control Battery")

			};
			ExperimentPageConfig b = new ExperimentPageConfig (visualizations, "Experiment","127.0.0.1",9001);

			PagesConfig c = WashingMachineExperiment ();

			return pageFactory.CreatePages (new List<MasterPageConfig> (){ b, c.Pages[0] });
		}
*/


	}
}

