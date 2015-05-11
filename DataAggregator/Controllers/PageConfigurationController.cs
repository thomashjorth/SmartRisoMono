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

			PageConfig page0 = new PageConfig (new List<VisualizationConfig> (){  
				new GraphConfig("127.0.0.1",9001,"Aggregation","AvgActivePower", 10000,"Title",-10,10,15,"unit"),
				new GraphConfig("127.0.0.1",9001,"Realtime","?host=127.0.0.1&port=8080&wsInterface=GenericLoadWS&resource=getActivePower",10000, "Title",-10,10,15,"unit"), 
				new BarConfig("127.0.0.1",9001,"Aggregation","AllActivePower",10000,"avg",-2,2,"unit"),
				new PieConfig("127.0.0.1",9001,"Aggregation","AllActivePower",10000,"avg"),
				new GaugesConfig(
					new GaugeConfig("127.0.0.1",9001,"Realtime","?host=127.0.0.1&port=8080&wsInterface=GenericLoadWS&resource=getActivePower",1000,"Title",-10,10,"gauge1","unit"),
					new GaugeConfig("127.0.0.1",9001,"Realtime","?host=127.0.0.1&port=8085&wsInterface=GenericLoadWS&resource=getActivePower",1000,"Title",-10,10,"gauge2","unit")),
				new BarConfig("127.0.0.1",9001,"Aggregation","AllActivePower",10000,"avg",-1,2,"unit"),
				new UnitConfig("localhost",9001,"?host=localhost&port=8080",3000),
				new UnitConfig("localhost",9001,"?host=localhost&port=8085",3000),
				new UnitConfig("localhost",9001,"?host=localhost&port=8090",3000)
			});
			PageConfig page1 = new PageConfig (new List<VisualizationConfig> (){ 
				new GaugesConfig(
					new GaugeConfig("127.0.0.1",9001,"Realtime","?host=127.0.0.1&port=8085&wsInterface=GenericLoadWS&resource=getActivePower",1000,"Title",-10,10,"gauge3","unit"),
					new GaugeConfig("127.0.0.1",9001,"Realtime","?host=127.0.0.1&port=8085&wsInterface=GenericLoadWS&resource=getActivePower",1000,"Title",-10,10,"gauge4","unit")),
				new GraphConfig("127.0.0.1",9001,"Realtime","?host=127.0.0.1&port=8080&wsInterface=GenericLoadWS&resource=getActivePower",10000, "Title",-10,10,15,"unit"), 
				new BarConfig("127.0.0.1",9001,"Aggregation","AllActivePower",10000,"avg",-2,2,"unit"),
				new PieConfig("127.0.0.1",9001,"Aggregation","AllActivePower",10000,"avg"),
				new GraphConfig("127.0.0.1",9001,"Realtime","?host=127.0.0.1&port=8085&wsInterface=GenericLoadWS&resource=getActivePower",10000, "Title",-10,10,15,"unit"), 
				new GaugesConfig(
					new GaugeConfig("127.0.0.1",9001,"Realtime","?host=127.0.0.1&port=8080&wsInterface=GenericLoadWS&resource=getActivePower",1000,"Title",-10,10,"gauge5","unit"),
					new GaugeConfig("127.0.0.1",9001,"Realtime","?host=127.0.0.1&port=8080&wsInterface=GenericLoadWS&resource=getActivePower",1000,"Title",-10,10,"gauge6","unit")),
				new UnitConfig("localhost",9001,"?host=localhost&port=8080",3000),
				new UnitConfig("localhost",9001,"?host=localhost&port=8085",3000),
				new UnitConfig("localhost",9001,"?host=localhost&port=8090",3000)
			});

			PagesConfig pages = new PagesConfig(new List<PageConfig>(){page0,page1});

			PageConfig page3 = new PageConfig (new List<VisualizationConfig> (){ 
				
				new PieConfig("127.0.0.1",9001,"Appliance","?item=Program&attribute=Count",2000,"Count"),
				new PieConfig("127.0.0.1",9001,"Appliance","?item=Program&attribute=PowerCentroid",2000,"Power"),
				new PieConfig("127.0.0.1",9001,"Appliance","?item=Program&attribute=EnergyCentroid",2000,"Energy"),
				new BarConfig("127.0.0.1",9001,"Appliance","?item=Program&attribute=Count",2000,"Programs Count",0,10,"Count"),
				new BarConfig("127.0.0.1",9001,"Appliance","?item=AEC&attribute=ALL",2000,"Programs AEC",0,400,"Count"),
				new BarConfig("127.0.0.1",9001,"Appliance","?item=Score&attribute=ALL",2000,"Programs Score",0,100,"Count"),
				new GraphConfig("127.0.0.1",9001,"Realtime",
					"?host=127.0.0.1&port=8080&wsInterface=GenericLoadWS&resource=getActivePower",2000, 
					"Power 15s",0,1,15,"mW"), 
				new GraphConfig("127.0.0.1",9001,"Realtime",
					"?host=127.0.0.1&port=8080&wsInterface=GenericLoadWS&resource=getActivePower",2000, 
					"Power 48h",0,1,3600*48,"mW"), 
				new TableConfig("127.0.0.1",9001,"Appliance","?item=Program&attribute=Discovered",2000,"Detected")
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
					HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject (emptyPage)
					);
				}


				response.Headers.Add ("Access-Control-Allow-Origin", "*");
				response.Headers.Add ("Access-Control-Allow-Methods", "GET");

				return response;
    }
	}}
