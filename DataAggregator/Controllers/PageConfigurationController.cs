using System.Web.Http;
using System.Net.Http;
using System.Collections.Generic;
using System.Net;
using System.IO;
using DataModel.ConfigurationModel;

namespace DataAggregator.Controllers
{
    public class PageConfigurationController : ApiController
    {
		public HttpResponseMessage Get(string id)
		{
			HttpResponseMessage response;

			/* Example 
			PageConfig page0 = new PageConfig (new List<VisualizationConfig> (){  
				new UnitConfig("localhost",9001,3000,new VisualizationConfig("localhost",8080))
			});*/

			VisualizationFactory VisFac = new VisualizationFactory ();

			MAggregationVisualizationFactory multi=
				VisFac.CreateMultiAggregationFactory ("127.0.0.1", 9001);


			SAggregationVisualizationFactory single =
				VisFac.CreateSingleAggregationFactory ("127.0.0.1", 9001);

			RealtimeVisualizationFactory realtime8080 = 
				//VisFac.CreateRealtimeVizualizationFactory ("127.0.0.1", 9001,"localhost", 8080);
				VisFac.CreateRealtimeVizualizationFactory ("192.168.0.117", 8080,"192.168.0.101", 8080);
			
			RealtimeVisualizationFactory realtime8085 = 
				//VisFac.CreateRealtimeVizualizationFactory ("127.0.0.1", 9001,"localhost", 8085);
				VisFac.CreateRealtimeVizualizationFactory ("192.168.0.117", 8080,"192.168.0.101", 8085);
			
			RealtimeVisualizationFactory realtime8090 = 
				//VisFac.CreateRealtimeVizualizationFactory ("127.0.0.1", 9001,"localhost", 8090);
				VisFac.CreateRealtimeVizualizationFactory ("192.168.0.117", 8080,"192.168.0.101", 8090);
			
			AbstractPageFactory pageFactory = new EquallySized3x3PageFactory ();

			PagesConfig loadGenerated = pageFactory.CreatePages(new List<VisualizationConfig> (){ 
				realtime8080.CreateGauge(RealtimeInterface.GenericLoadWS, RealtimeData.ActivePower,1000,"Active Power",0,100,"P [kW]",
					new double[,]{{0,80}},null,new double[,]{{80,100}}),
				realtime8080.CreateGauge(RealtimeInterface.GenericLoadWS, RealtimeData.ReactivePower,1000,"Reactive Power",-13,13,"Q [kVAr]",
					new double[,]{{-10,10}},null,new double[,]{{-13,-10},{10,13}}),
				realtime8080.CreateGauge(RealtimeInterface.GenericLoadWS, RealtimeData.Frequency,1000,"Frequency",45,55,"f [Hz]",
					new double[,]{{47.5,52.5}},null,new double[,]{{45.0,47.5},{52.5,55}}),
				realtime8080.CreateGauge(RealtimeInterface.GenericLoadWS, RealtimeData.InterphaseVoltages,1000,"Interphase Voltages",0,500,"U [V]",
					new double[,]{{360,440}},null,new double[,]{{0,360},{440,500}}),
				realtime8080.CreateGauge(RealtimeInterface.GenericLoadWS, RealtimeData.Temperature,1000,"Temperature",0,300,"T [degC]",
					new double[,]{{0.0,100}},new double[,]{{100,150}},new double[,]{{150,300}}),
				realtime8080.CreateControl(RealtimeInterface.GenericLoadWS,"dumploadControl"),
				realtime8080.CreateUnit(30000)
			});

			PagesConfig gaiaGenerated = pageFactory.CreatePages(new List<VisualizationConfig> (){ 
				realtime8085.CreateGauge(RealtimeInterface.GaiaWindTurbineWS, RealtimeData.InterphaseVoltages,1000,"Interphase Voltages",0,500,"U [V]",
					new double[,]{{350,450}},null,new double[,]{{0,350},{450,500}}),
				realtime8085.CreateGauge(RealtimeInterface.GaiaWindTurbineWS, RealtimeData.ActivePower,1000,"Active Power",-5,20,"P [kW]",
					new double[,]{{0,11}},new double[,]{{-5,0},{11,15}},new double[,]{{15,20}}),
				realtime8085.CreateGauge(RealtimeInterface.GaiaWindTurbineWS, RealtimeData.WindspeedOutsideNacelle,1000,"Windspeed",0,40,"v [m/s]",
					new double[,]{{0,25}},new double[,]{{25,39}},new double[,]{{39,40}}),
				realtime8085.CreateGauge(RealtimeInterface.GaiaWindTurbineWS, RealtimeData.RotorRPM,1000,"Rotor RPM",0,100,"RPM0 [1/min]",
					new double[,]{{0,62}},new double[,]{{62,70}},new double[,]{{70,100}}),
				realtime8085.CreateGauge(RealtimeInterface.GaiaWindTurbineWS, RealtimeData.GeneratorRPM,1000,"Generator RPM",0,1200,"RPM1 [1/min]",
					new double[,]{{0,1040}},new double[,]{{1040,1100}},new double[,]{{1100,1200}}),
				realtime8085.CreateGraph(RealtimeInterface.GaiaWindTurbineWS, RealtimeData.InterphaseVoltages,5000,"Interphase Voltages",0,500,12,"U [V]"),
				realtime8085.CreateControl(RealtimeInterface.GaiaWindTurbineWS,"gaiaControl"),
				realtime8085.CreateUnit(30000)
			});

			AbstractApplianceVisualizationFactory appliance = VisFac.CreateApplianceVizualizationFactory ("127.0.0.1", 9001);

			PagesConfig washingExample = pageFactory.CreatePages (new List<VisualizationConfig> (){ 
				
				appliance.CreatePie(ApplianceData.Count,2000,"Count"),
				appliance.CreatePie(ApplianceData.PowerCentroid,2000,"Power"),
				appliance.CreatePie(ApplianceData.EnergyCentroid,2000,"Energy"),
				appliance.CreateBar(ApplianceData.Count,2000,"Count",0,10,"#"),
				appliance.CreateBar(ApplianceData.AEC,2000,"AEC",0,400,"kWh"),
				appliance.CreateBar(ApplianceData.Score, 2000,"Score",0,100,""),
				realtime8080.CreateGraph(RealtimeInterface.GenericLoadWS, RealtimeData.ActivePower,2000,"Active Power",-1,1,15,"mW"), 
				realtime8080.CreateGraph(RealtimeInterface.GenericLoadWS, RealtimeData.ActivePower,2000,"Active Power",-1,1,7200,"mW"),
				appliance.CreateTable(ApplianceData.Discovered, 2000,"Discovered Programs")
				//realtime8080.CreateGauge(RealtimeInterface.GenericLoadWS, RealtimeData.ActivePower,2000,"test",-1,1,"W")
				}
				

			);

				try{
					StreamReader file = File.OpenText (@"PageConfigurations/"+id+".json");
					string read = file.ReadToEnd();
					read = read.Replace("\\","");
					read = read.Remove(0,1);
					read = read.Remove(read.Length-1,1);

					response = Request.CreateResponse (
						HttpStatusCode.Created, 
							read

					);
					
				}catch{

				PagesConfig pages = new PagesConfig (gaiaGenerated.Pages);
				pages.addPagesConfig (loadGenerated);
					response = Request.CreateResponse (
					HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject (
						pages
					)
					);
				}


				response.Headers.Add ("Access-Control-Allow-Origin", "*");
				response.Headers.Add ("Access-Control-Allow-Methods", "GET");

				return response;
    }
	}}
