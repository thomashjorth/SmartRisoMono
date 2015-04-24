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
				new GraphConfig("127.0.0.1",9001,"Aggregation","AvgActivePower","Title",4,-10,10,15,
					null,1000,"unit"),
				new GraphConfig("127.0.0.1",9001,"Realtime","getActivePower","Title",4,-10,10,15,
					new VisualizationConfig("127.0.0.1",8080,"GenericLoadWS","getActivePower",""),1000,"unit"), 
				new BarConfig("127.0.0.1",9001,"Aggregation","AllActivePower","avg",-2,2,10000,"unit"),
				new PieConfig("127.0.0.1",9001,"Aggregation","AllActivePower","avg",10000),
				new GaugesConfig(
					new GaugeConfig("127.0.0.1",9001,"Realtime","getActivePower","Title",-10,10,"gauge1", 
						new VisualizationConfig("127.0.0.1",8080,"GenericLoadWS","getActivePower",""),1000,"unit"),
					new GaugeConfig("127.0.0.1",9001,"Realtime","getActivePower","Title",-10,10,"gauge2", 
						new VisualizationConfig("127.0.0.1",8085,"GenericLoadWS","getActivePower",""),1000,"unit")),
				new BarConfig("127.0.0.1",9001,"Aggregation","AllActivePower","avg",-1,2,10000,"unit")
			});
			PageConfig page1 = new PageConfig (new List<VisualizationConfig> (){ 
				new GaugesConfig(
					new GaugeConfig("127.0.0.1",9001,"Realtime","getActivePower","Title",-10,10,"gauge5", 
						new VisualizationConfig("127.0.0.1",8080,"GenericLoadWS","getActivePower",""),1000,"unit"),
					new GaugeConfig("127.0.0.1",9001,"Realtime","getActivePower","Title",-10,10,"gauge6", 
						new VisualizationConfig("127.0.0.1",8085,"GenericLoadWS","getActivePower",""),1000,"unit")),
				new GraphConfig("127.0.0.1",9001,"Aggregation","AvgActivePower","Title",4,-10,10,15,
					null,1000,"unit"),
				new GaugesConfig(
					new GaugeConfig("127.0.0.1",9001,"Realtime","getActivePower","Title",-10,10,"gauge3", 
						new VisualizationConfig("127.0.0.1",8080,"GenericLoadWS","getActivePower",""),1000,"unit"),
					new GaugeConfig("127.0.0.1",9001,"Realtime","getActivePower","Title",-10,10,"gauge4", 
						new VisualizationConfig("127.0.0.1",8085,"GenericLoadWS","getActivePower",""),1000,"unit")),
				new PieConfig("127.0.0.1",9001,"Aggregation","AllActivePower","avg",10000),
				new BarConfig("127.0.0.1",9001,"Aggregation","AllActivePower","avg",-2,2,10000,"unit"),
				new BarConfig("127.0.0.1",9001,"Aggregation","AllActivePower","avg",-1,2,10000,"unit")
			});

			PagesConfig pages = new PagesConfig(new List<PageConfig>(){page0,page1});

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
