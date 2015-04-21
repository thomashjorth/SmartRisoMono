using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using DataAggregator.Models;
using DataModel;

namespace DataAggregator.Controllers
{
    public class EEIController : ApiController
    {
		// GET: hostname:port/api/Aggregation/id
		public HttpResponseMessage Get(string id)
		{
			HttpResponseMessage response;
			if (id.Equals ("WashingCyclesWeek")) {
				List<LabeledInstance> WashingCount = new List<LabeledInstance> () { 
					new LabeledInstance ("20 ℃ Half Load", 1),
					new LabeledInstance ("20 ℃ Full Load", 3),
					new LabeledInstance ("40 ℃ Half Load", 4),
					new LabeledInstance ("40 ℃ Full Load", 6),
					new LabeledInstance ("60 ℃ Half Load", 12),
					new LabeledInstance ("60 ℃ Full Load", 2),
					new LabeledInstance ("90 ℃ Half Load", 0),
					new LabeledInstance ("90 ℃ Full Load", 0),
				};
				response = Request.CreateResponse (
					               HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject (
					               WashingCount
				               )
				               );
				response.Headers.Add ("Access-Control-Allow-Origin", "*");
				response.Headers.Add ("Access-Control-Allow-Methods", "GET");

				return response;
			}else if (id.Equals ("WashingCyclesMonth")) {
				List<LabeledInstance> WashingCount = new List<LabeledInstance> () { 
					new LabeledInstance ("20 ℃ Half Load", 10),
					new LabeledInstance ("20 ℃ Full Load", 30),
					new LabeledInstance ("40 ℃ Half Load", 30),
					new LabeledInstance ("40 ℃ Full Load", 64),
					new LabeledInstance ("60 ℃ Half Load", 12),
					new LabeledInstance ("60 ℃ Full Load", 22),
					new LabeledInstance ("90 ℃ Half Load", 1),
					new LabeledInstance ("90 ℃ Full Load", 5),
				};
				response = Request.CreateResponse (
					HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject (
						WashingCount
					)
				);
				response.Headers.Add ("Access-Control-Allow-Origin", "*");
				response.Headers.Add ("Access-Control-Allow-Methods", "GET");

				return response;
			}else if (id.Equals ("WashingCyclesYear")) {
				List<LabeledInstance> WashingCount = new List<LabeledInstance> () { 
					new LabeledInstance ("20 ℃ Half Load", 100),
					new LabeledInstance ("20 ℃ Full Load", 32),
					new LabeledInstance ("40 ℃ Half Load", 42),
					new LabeledInstance ("40 ℃ Full Load", 63),
					new LabeledInstance ("60 ℃ Half Load", 142),
					new LabeledInstance ("60 ℃ Full Load", 21),
					new LabeledInstance ("90 ℃ Half Load", 10),
					new LabeledInstance ("90 ℃ Full Load", 10),
				};
				response = Request.CreateResponse (
					HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject (
						WashingCount
					)
				);
				response.Headers.Add ("Access-Control-Allow-Origin", "*");
				response.Headers.Add ("Access-Control-Allow-Methods", "GET");

				return response;
			}else if (id.Equals ("TotalCyclesWeek")) {
				response = Request.CreateResponse (
					HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject (
						new CompositeMeasurement(10)
					)
				);
				response.Headers.Add ("Access-Control-Allow-Origin", "*");
				response.Headers.Add ("Access-Control-Allow-Methods", "GET");

				return response;
			}else if (id.Equals ("TotalCyclesMonth")) {
				response = Request.CreateResponse (
					HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject (
						new CompositeMeasurement(10)
					)
				);
				response.Headers.Add ("Access-Control-Allow-Origin", "*");
				response.Headers.Add ("Access-Control-Allow-Methods", "GET");

				return response;
			}else if (id.Equals ("TotalCyclesYear")) {
				response = Request.CreateResponse (
					HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject (
						new CompositeMeasurement(10)
					)
				);
				response.Headers.Add ("Access-Control-Allow-Origin", "*");
				response.Headers.Add ("Access-Control-Allow-Methods", "GET");

				return response;
			}else if (id.Equals ("Rating")) {
				string Rating = "A++";
				response = Request.CreateResponse (
					HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject (
						Rating
					)
				);
				response.Headers.Add ("Access-Control-Allow-Origin", "*");
				response.Headers.Add ("Access-Control-Allow-Methods", "GET");

				return response;
			}else if (id.Equals ("Score")) {
				
				response = Request.CreateResponse (
					HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject (
						new CompositeMeasurement(80.1)
					)
				);
				response.Headers.Add ("Access-Control-Allow-Origin", "*");
				response.Headers.Add ("Access-Control-Allow-Methods", "GET");

				return response;
			}else if (id.Equals ("AEC")) {
				
				response = Request.CreateResponse (
					HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject (
						new CompositeMeasurement(101)
					)
				);
				response.Headers.Add ("Access-Control-Allow-Origin", "*");
				response.Headers.Add ("Access-Control-Allow-Methods", "GET");

				return response;
			}else if (id.Equals ("StandbyConsumption")) {
				
				response = Request.CreateResponse (
					HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject (
						new CompositeMeasurement(234)
					)
				);
				response.Headers.Add ("Access-Control-Allow-Origin", "*");
				response.Headers.Add ("Access-Control-Allow-Methods", "GET");

				return response;
			}
					response = Request.CreateResponse (
					HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject (
					"WashingCycles <List<LabeledInstance>>, TotalCycles <CompositeMeasurement>, Rating <String>, Score <CompositeMeasurement>, AEC <CompositeMeasurement>"
					)
				);
				response.Headers.Add ("Access-Control-Allow-Origin", "*");
				response.Headers.Add ("Access-Control-Allow-Methods", "GET");
			return response;

		}
    }
}
