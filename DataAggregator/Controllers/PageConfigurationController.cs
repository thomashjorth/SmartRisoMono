using System;
using System.Web.Http;
using System.Net.Http;
using System.Collections.Generic;
using System.Net;
using System.IO;

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

			PageConfig page0 = new PageConfig (new List<VisualizationConfig> (){  
				new GraphConfig("127.0.0.1",9001,"Aggregation","AvgActivePower","Title",4,-10,10,15,
					null,10000,"unit"),
				new GraphConfig("127.0.0.1",9001,"Realtime","getActivePower","Title",4,-10,10,15,
					new VisualizationConfig("127.0.0.1",8080,"GenericLoadWS","getActivePower",""),10000,"unit"), 
				new BarConfig("127.0.0.1",9001,"Aggregation","AllActivePower","avg",-2,2,10000,"unit"),
				new PieConfig("127.0.0.1",9001,"Aggregation","AllActivePower","avg",10000),
				new GaugesConfig(
					new GaugeConfig("127.0.0.1",9001,"Realtime","getActivePower","Title",-10,10,"gauge1", 
						new VisualizationConfig("127.0.0.1",8080,"GenericLoadWS","getActivePower",""),1000,"unit"),
					new GaugeConfig("127.0.0.1",9001,"Realtime","getActivePower","Title",-10,10,"gauge2", 
						new VisualizationConfig("127.0.0.1",8085,"GenericLoadWS","getActivePower",""),1000,"unit")),
				new BarConfig("127.0.0.1",9001,"Aggregation","AllActivePower","avg",-1,2,10000,"unit"),
				new UnitConfig("localhost",9001,3000,new VisualizationConfig("localhost",8080)),
				new UnitConfig("localhost",9001,3000,new VisualizationConfig("localhost",8080)),
				new UnitConfig("localhost",9001,3000,new VisualizationConfig("localhost",8080))
			});
			PageConfig page1 = new PageConfig (new List<VisualizationConfig> (){ 
				new GaugesConfig(
					new GaugeConfig("127.0.0.1",9001,"Realtime","getActivePower","Title",-10,10,"gauge5", 
						new VisualizationConfig("127.0.0.1",8080,"GenericLoadWS","getActivePower",""),1000,"unit"),
					new GaugeConfig("127.0.0.1",9001,"Realtime","getActivePower","Title",-10,10,"gauge6", 
						new VisualizationConfig("127.0.0.1",8085,"GenericLoadWS","getActivePower",""),1000,"unit")),
				new GraphConfig("127.0.0.1",9001,"Aggregation","AvgActivePower","Title",4,-10,10,15,
					null,10000,"unit"),
				new GaugesConfig(
					new GaugeConfig("127.0.0.1",9001,"Realtime","getActivePower","Title",-10,10,"gauge3", 
						new VisualizationConfig("127.0.0.1",8080,"GenericLoadWS","getActivePower",""),1000,"unit"),
					new GaugeConfig("127.0.0.1",9001,"Realtime"
						,"getActivePower","Title",-10,10,"gauge4", 
						new VisualizationConfig("127.0.0.1",8085,"GenericLoadWS","getActivePower",""),1000,"unit")),
				new PieConfig("127.0.0.1",9001,"Aggregation","AllActivePower","avg",10000),
				new BarConfig("127.0.0.1",9001,"Aggregation","AllActivePower","avg",-2,2,10000,"unit"),
				new BarConfig("127.0.0.1",9001,"Aggregation","AllActivePower","avg",-1,2,10000,"unit"),
				new UnitConfig("localhost",9001,3000,new VisualizationConfig("localhost",8080)),
				new UnitConfig("localhost",9001,3000,new VisualizationConfig("localhost",8080)),
				new UnitConfig("localhost",9001,3000,new VisualizationConfig("localhost",8080))
			});

			PagesConfig pages = new PagesConfig(new List<PageConfig>(){page0,page1});

			PageConfig page3 = new PageConfig (new List<VisualizationConfig> (){ 
				
				new PieConfig("127.0.0.1",9001,"WashingCycle","Count","Count",2000),
				new PieConfig("127.0.0.1",9001,"WashingCycle","PowerCentroid","Power",2000),
				new PieConfig("127.0.0.1",9001,"WashingCycle","EnergyCentroid","Energy (kWh)",2000),
				new BarConfig("127.0.0.1",9001,"WashingCycle","Count","Programs Count",0,10,2000,"Count"),
				new BarConfig("127.0.0.1",9001,"EEI","?id=AEC&situation=ALL","Programs AEC",0,400,2000,"Count"),
				new BarConfig("127.0.0.1",9001,"EEI","?id=Score&situation=ALL","Programs Score",0,100,2000,"Count"),
				new GraphConfig("127.0.0.1",9001,"Realtime","getActivePower","Power 15s",4,0,1,15,
					new VisualizationConfig("127.0.0.1",8080,"GenericLoadWS","getActivePower",""),2000,"mW"),
				new GraphConfig("127.0.0.1",9001,"Realtime","getActivePower","Power 48h",4,0,1,3600*48,
					new VisualizationConfig("127.0.0.1",8080,"GenericLoadWS","getActivePower",""),2000,"mW"),
				new TableConfig("127.0.0.1",9001,"WashingCycle","Discovered","Detected",2000)
				}
				

			);
				
			PagesConfig pagesWashingMachine = new PagesConfig(new List<PageConfig>(){page3});

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
				PageConfig emptyPage = new PageConfig (new List<VisualizationConfig> (){  });
				PagesConfig pagesEmpty = new PagesConfig(new List<PageConfig>(){emptyPage});
					response = Request.CreateResponse (
					HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject (pagesWashingMachine)
					);
				}


				response.Headers.Add ("Access-Control-Allow-Origin", "*");
				response.Headers.Add ("Access-Control-Allow-Methods", "GET");

				return response;
    }
	}}
