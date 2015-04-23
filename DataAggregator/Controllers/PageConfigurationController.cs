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
				new GraphConfig("127.0.0.1",9001,"Aggregation","AvgActivePower","realtime",10,-10,10,15),
				new GraphConfig("127.0.0.1",9001,"Realtime","getActivePower","realtime",10,-10,10,15), 
		
				new BarConfig("127.0.0.1",9001,"Aggregation","AllActivePower","avg",-2,2),
				new PieConfig("127.0.0.1",9001,"Aggregation","AllActivePower","avg"),
				new GaugesConfig(
					new GaugeConfig("127.0.0.1",9001,"Realtime","getActivePower","realtime0",-10,10,"gauge1", 
						new VisualizationConfig("127.0.0.1",8080,"GenericLoadWS","getActivePower","")),
					new GaugeConfig("127.0.0.1",9001,"Realtime","getActivePower","realtime1",-10,10,"gauge2", 
						new VisualizationConfig("127.0.0.1",8085,"GenericLoadWS","getActivePower",""))),
				new BarConfig("127.0.0.1",9001,"Aggregation","AllActivePower","avg",-1,2)
			});*/
			/*PageConfig page1 = new PageConfig (new List<VisualizationConfig> (){  
				new GraphConfig("localhost",8080,"Aggregation","AvgActivePower","Testtest",10,-10,10,15),
				new GraphConfig("localhost",8080,"Realtime","getActivePower","realtime",10,-10,10,15),
				new GaugesConfig("127.0.0.1",9001,"Realtime","getActivePower","realtime",
					new GaugeConfig("some 1",-10,10,"gauge1", 
						new VisualizationConfig("127.0.0.1",8080,"GenericLoadWS","getActivePower","")),
					new GaugeConfig("dgd",-10,10,"gauge1", 
						new VisualizationConfig("127.0.0.1",8080,"GenericLoadWS","getActivePower","")))
					}
			);
			PagesConfig pages = new PagesConfig(new List<PageConfig>(){page0});
		*/
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
						HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject ("")
					);
				}


				response.Headers.Add ("Access-Control-Allow-Origin", "*");
				response.Headers.Add ("Access-Control-Allow-Methods", "GET");

				return response;
    }
	}}
