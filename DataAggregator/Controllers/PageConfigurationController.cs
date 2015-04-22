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

			/* Example*/
			PageConfig page0 = new PageConfig (new List<VisualizationConfig> (){  
				new GraphConfig("localhost",8080,"Aggregation","AvgActivePower","realtime",10,-10,10),
				new GraphConfig("localhost",8080,"Realtime","getActivePower","realtime",10,-10,10), 
				new GaugeConfig("localhost",8080,"Realtime","getActivePower","realtime",-10,10,"gauge1"),
				new GaugeConfig("localhost",8080,"Aggregation","AvgActivePower","avg",-1,11,"gauge2"),
				new PieConfig("localhost",8080,"Aggregation","AvgActivePower","avg"),
				new BarConfig("localhost",8080,"Aggregation","AvgActivePower","avg",-1,2)
			});
			PageConfig page1 = new PageConfig (new List<VisualizationConfig> (){  
				new GraphConfig("localhost",8080,"Aggregation","AvgActivePower","Testtest",10,-10,10),
				new GraphConfig("localhost",8080,"Realtime","getActivePower","realtime",10,-10,10)}
			);
			PagesConfig pages = new PagesConfig(new List<PageConfig>(){page0});

				try{
					StreamReader file = File.OpenText (@"PageConfigurations/"+id+".json");
					string read = file.ReadToEnd();
					read.Replace("\\"," ");

					response = Request.CreateResponse (
						HttpStatusCode.Created, 
							read

					);
					
				}catch{
				PageConfig emptyPage = new PageConfig (new List<VisualizationConfig> (){  });
				PagesConfig pagesEmpty = new PagesConfig(new List<PageConfig>(){emptyPage});
					response = Request.CreateResponse (
						HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject (
						//pagesEmpty
						pages
						)
					);
				}


				response.Headers.Add ("Access-Control-Allow-Origin", "*");
				response.Headers.Add ("Access-Control-Allow-Methods", "GET");

				return response;
    }
	}}