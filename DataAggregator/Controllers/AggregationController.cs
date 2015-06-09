using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DataAggregator.Utils;
using System.Net.Http;
using System.Net;
using DataModel;

namespace DataAggregator.Controllers
{
    public class AggregationController : ApiController
    {
		List<string> functions = new List<string> { 
			"DeviceList", "AvgActivePower", "AllActivePower"
		};
		public HttpResponseMessage Get(
			[FromUri] string host, 
			[FromUri] string port )
		{
			HttpResponseMessage response;

			DER der = new DER (host, port);

			response = Request.CreateResponse (
				HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject (
					der
				));

			response.Headers.Add ("Access-Control-Allow-Origin", "*");
			response.Headers.Add ("Access-Control-Allow-Methods", "GET");

			return response;
		}
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

			// "AllActivePower"
			if (id == functions.ElementAt (2)) {
				var response = Request.CreateResponse (
					HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject(
						SimpleStatistics.AllActivePower(ders)
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
		[HttpGet]
		public HttpResponseMessage GetUnitsMerged(
			[FromUri] string hosts, 
			[FromUri] string wsInterface, 
			[FromUri] string resource)
		{
			HttpResponseMessage response;

			var units = hosts.Split (';').ToList();

			string data = "NAN";
			if(units.Count() > 0)
				data = Newtonsoft.Json.JsonConvert.SerializeObject (UnitMerging.CombineData (units, wsInterface, resource));
			response = Request.CreateResponse(HttpStatusCode.Created,data);

			response.Headers.Add("Access-Control-Allow-Origin", "*");
			response.Headers.Add("Access-Control-Allow-Methods", "GET");
			return response;
		}

		[HttpGet]
		public HttpResponseMessage GetUnits(
			[FromUri] string hosts, 
			[FromUri] string wsInterfaces, 
			[FromUri] string resources)
		{
			HttpResponseMessage response;
			
			var units = hosts.Split (';').ToList();
			var devices = wsInterfaces.Split (';').ToList();
			var actions = resources.Split (';').ToList();

			string data = "NAN";
			if(units.Count() > 0)
				data = Newtonsoft.Json.JsonConvert.SerializeObject (UnitMerging.GetData (units, devices, actions));
			response = Request.CreateResponse(HttpStatusCode.Created,data);

			response.Headers.Add("Access-Control-Allow-Origin", "*");
			response.Headers.Add("Access-Control-Allow-Methods", "GET");
			return response;
		}
	}}