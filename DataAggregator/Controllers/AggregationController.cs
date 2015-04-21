using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using DataAggregator.Models;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Net;

namespace DataAggregator.Controllers
{
    public class AggregationController : ApiController
    {
		List<string> functions = new List<string> { 
			"DeviceList", "AvgActivePower", "AllPosActivePower", "AllNegActivePower"
		};

		// GET: hostname:port/api/Aggregation/id
		public HttpResponseMessage Get(string id)
		{
			List<DER> ders = Utils.Configuration.DerConfig (true);

			if (id == functions.ElementAt (0)) {
				var response = Request.CreateResponse (HttpStatusCode.Created,Newtonsoft.Json.JsonConvert.SerializeObject(ders));
				response.Headers.Add("Access-Control-Allow-Origin", "*");
				response.Headers.Add("Access-Control-Allow-Methods", "GET");
				return response;
			} 

			// "AvgActivePower"
			if (id == functions.ElementAt (1)) {
				var response = Request.CreateResponse (
					HttpStatusCode.Created, 
					Newtonsoft.Json.JsonConvert.SerializeObject(SimpleStatistics.AvgActivePower(ders)));
				response.Headers.Add("Access-Control-Allow-Origin", "*");
				response.Headers.Add("Access-Control-Allow-Methods", "GET");
				return response;
			} 
			// "AllPosActivePower"
			if (id == functions.ElementAt (2)) {
				var response = Request.CreateResponse (
					HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject(
						SimpleStatistics.AllActivePower(ders, 1)
					)
				);
				response.Headers.Add("Access-Control-Allow-Origin", "*");
				response.Headers.Add("Access-Control-Allow-Methods", "GET");

				return response;
			} 
			// "AllNegActivePower"
			if (id == functions.ElementAt (3)) {
				var response = Request.CreateResponse (
					HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject(
						SimpleStatistics.AllActivePower(ders, -1)
					)
				);
				response.Headers.Add("Access-Control-Allow-Origin", "*");
				response.Headers.Add("Access-Control-Allow-Methods", "GET");

				return response;
			} 

			// Wrong id:
			else {
				string output= "";
				foreach (string s in functions) {
					output = output + " : " + s;
				}
				var response = Request.CreateResponse (HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject(id + " : is not a function supported by this Web Service. Try: " + output));
				response.Headers.Add("Access-Control-Allow-Origin", "*");
				response.Headers.Add("Access-Control-Allow-Methods", "GET");
				return response;
			}

		}
	}}