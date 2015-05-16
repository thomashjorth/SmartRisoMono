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
				VisFac.CreateRealtimeVizualizationFactory ("127.0.0.1", 9001,"localhost", 8080);
			
			RealtimeVisualizationFactory realtime8085 = 
				VisFac.CreateRealtimeVizualizationFactory ("127.0.0.1", 9001,"localhost", 8085);
			
			RealtimeVisualizationFactory realtime8090 = 
				VisFac.CreateRealtimeVizualizationFactory ("127.0.0.1", 9001,"localhost", 8090);
			
			AbstractPageFactory pageFactory = new EquallySized3x3PageFactory ();

			PagesConfig loadGenerated = pageFactory.CreatePages(new List<VisualizationConfig> (){ 
				realtime8080.CreateGauge(RealtimeInterface.GenericLoadWS, RealtimeData.ActivePower,1000,"Active Power",0,100,"P [kW]"),
				realtime8080.CreateGauge(RealtimeInterface.GenericLoadWS, RealtimeData.ReactivePower,1000,"Reactive Power",-13,13,"Q [kVAr]"),
				realtime8080.CreateGauge(RealtimeInterface.GenericLoadWS, RealtimeData.Frequency,1000,"Frequency",45,55,"f [Hz]"),
				realtime8080.CreateGauge(RealtimeInterface.GenericLoadWS, RealtimeData.InterphaseVoltages,1000,"Interphase Voltages",0,500,"U [V]"),
				realtime8080.CreateGauge(RealtimeInterface.GenericLoadWS, RealtimeData.Temperature,1000,"Temperature",0,300,"T [degC]"),
				realtime8080.CreateControl(RealtimeInterface.GenericLoadWS),
				realtime8080.CreateUnit(30000)

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
				appliance.CreateTable(ApplianceData.Discovered, 2000,"Discovered Programs"),
				realtime8080.CreateGauge(RealtimeInterface.GenericLoadWS, RealtimeData.ActivePower,2000,"test",-1,1,"W")
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
					response = Request.CreateResponse (
					HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject (
					//loadGenerated
					  washingExample
					)
					);
				}


				response.Headers.Add ("Access-Control-Allow-Origin", "*");
				response.Headers.Add ("Access-Control-Allow-Methods", "GET");

				return response;
    }
	}}
