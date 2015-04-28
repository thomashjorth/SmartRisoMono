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

			/* Example */
			PageConfig page0 = new PageConfig (new List<VisualizationConfig> (){  
				new UnitConfig("localhost",9001,3000,new VisualizationConfig("localhost",8080))
			});

			PageConfig page1 = new PageConfig (new List<VisualizationConfig> (){  
				new UnitConfig("localhost",9001,3000,new VisualizationConfig("localhost",8080)),
				new GraphConfig(
					"127.0.0.1",
					9001,
					"Realtime",
					"getActivePower",
					"Title",
					4,
					-10,
					10,
					15,
					new VisualizationConfig("127.0.0.1",8080,"GenericLoadWS","getActivePower",""),
					10000, "sdf"
				), 
				new BarConfig(
					"127.0.0.1",
					9001,
					"Aggregation",
					"AllActivePower",
					"avg",
					-2,
					2,
					10000,"sddf"
				),
				new PieConfig("127.0.0.1",9001,"Aggregation","AllActivePower","avg",10000),
				new GaugesConfig(
					new GaugeConfig("127.0.0.1",9001,"Realtime","getActivePower","Title",-10,10,"gauge1", 
						new VisualizationConfig("127.0.0.1",8080,"GenericLoadWS","getActivePower",""),1000,"hh"),
					new GaugeConfig("127.0.0.1",9001,"Realtime","getActivePower","Title",-10,10,"gauge2", 
						new VisualizationConfig("127.0.0.1",8085,"GenericLoadWS","getActivePower",""),1000,"dsf")),
				new BarConfig("127.0.0.1",9001,"Aggregation","AllActivePower","avg",-1,2,10000, "dsf")
			});

			PagesConfig pages = new PagesConfig(new List<PageConfig>(){page0});

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
						HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject (pages)
					);
				}


				response.Headers.Add ("Access-Control-Allow-Origin", "*");
				response.Headers.Add ("Access-Control-Allow-Methods", "GET");

				return response;
    }
	}}
