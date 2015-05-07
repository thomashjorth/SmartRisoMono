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
			if (id.Equals ("Rating")) {
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
