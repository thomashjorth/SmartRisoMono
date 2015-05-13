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

			PagesConfig facGenerated = pageFactory.Create3x3Pages(new List<VisualizationConfig> (){ 
				single.CreateGraph (SingleAggregation.AvgActivePower,1000, "Title", -10, 10, 15, "unit"),
				realtime8080.CreateGraph(RealtimeInterface.GenericLoadWS, RealtimeData.ActivePower,10000, "Title",-10,10,15,"unit"), 
				multi.CreateBar(MultiAggregation.AllActivePower,10000,"All",-2,2,"unit"),
				multi.CreatePie(MultiAggregation.AllActivePower,10000,"All"),
				realtime8080.CreateGauge(RealtimeInterface.GenericLoadWS, RealtimeData.ActivePower,3000,"8080",0,1,"W"),
				multi.CreateBar(MultiAggregation.AllActivePower,10000,"All",-1,1,"unit"),
				realtime8080.CreateUnit(3000),
				realtime8085.CreateUnit(3000),
				realtime8090.CreateUnit(3000)	,
				realtime8085.CreateGauge(RealtimeInterface.GenericLoadWS, RealtimeData.ActivePower,3000,"8080",0,1,"W"),
				realtime8080.CreateGraph(RealtimeInterface.GenericLoadWS, RealtimeData.ActivePower,3000,"8080",0,1,10,"W"),
				multi.CreateBar(MultiAggregation.AllActivePower,3000,"All Active Power",0,1,"W"),
				realtime8085.CreateGraph(RealtimeInterface.GenericLoadWS, RealtimeData.ActivePower,3000,"8080",0,1,10,"W"),
				realtime8085.CreateGauge(RealtimeInterface.GenericLoadWS, RealtimeData.ActivePower,3000,"8080",0,1,"W"),
				realtime8090.CreateGauge(RealtimeInterface.GenericLoadWS, RealtimeData.ActivePower,3000,"8080",0,1,"W"),
				realtime8080.CreateUnit(3000),
				realtime8085.CreateUnit(3000),
				realtime8090.CreateUnit(3000),
				multi.CreatePie(MultiAggregation.AllActivePower,3000,"All Active Power")

			});

			AbstractApplianceVisualizationFactory appliance = VisFac.CreateApplianceVizualizationFactory ("127.0.0.1", 9001);

			PagesConfig washingExample = pageFactory.Create3x3Pages (new List<VisualizationConfig> (){ 
				
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
						facGenerated
					//  washingMachine
					)
					);
				}


				response.Headers.Add ("Access-Control-Allow-Origin", "*");
				response.Headers.Add ("Access-Control-Allow-Methods", "GET");

				return response;
    }
	}}
