using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAggregator.Utils;
using System.Threading.Tasks;

namespace DataAggregator.Controllers
{
	public class RealtimeController : ApiController
	{
		// GET: api/Realtime/5
		[HttpGet]
		public HttpResponseMessage GetCompositeMeasurement(
			[FromUri] string host, 
			[FromUri] string port, 
			[FromUri] string wsInterface, 
			[FromUri] string resource)
		{
			HttpResponseMessage response;
			/* Example use
			 * http://127.0.0.1:9001/api/Realtime/?host=localhost&port=8080&wsInterface=GenericLoadWS&resource=getActivePower
			*/
			string data = "NAN";
			if(resource == "getInterphaseVoltages")
				data = WS.GetCompositeMeasurementList (wsInterface, resource, host, port);
			else
				data = WS.GetCompositeMeasurement (wsInterface, resource, host, port);
			response = Request.CreateResponse(HttpStatusCode.Created,data);
			//Double.Parse (Utils.WS.DownloadXML (id,"localhost","8085"))));
			response.Headers.Add("Access-Control-Allow-Origin", "*");
			response.Headers.Add("Access-Control-Allow-Methods", "GET");
			return response;
		}

		[HttpGet]
		public HttpResponseMessage GetCompositeBoolean(
			[FromUri] string host, 
			[FromUri] string port, 
			[FromUri] string wsInterface, 
			[FromUri] string resource)
		{
			HttpResponseMessage response;
			string data = WS.GetCompositeBoolean (wsInterface, resource, host, port);
			response = Request.CreateResponse(HttpStatusCode.Created,data);
			//Double.Parse (Utils.WS.DownloadXML (id,"localhost","8085"))));
			response.Headers.Add("Access-Control-Allow-Origin", "*");
			response.Headers.Add("Access-Control-Allow-Methods", "GET");
			return response;
		}

		[HttpGet]
		public HttpResponseMessage GetStatus(
			[FromUri] string host, 
			[FromUri] string port, 
			[FromUri] string wsInterface, 
			[FromUri] string resource)
		{
			HttpResponseMessage response;
			string data = WS.GetStatus (wsInterface, resource, host, port);
			response = Request.CreateResponse(HttpStatusCode.Created,data);
			//Double.Parse (Utils.WS.DownloadXML (id,"localhost","8085"))));
			response.Headers.Add("Access-Control-Allow-Origin", "*");
			response.Headers.Add("Access-Control-Allow-Methods", "GET");
			return response;
		}

		[HttpPut]
		[AcceptVerbs("OPTIONS")]
		public HttpResponseMessage Put([FromUri] string host, [FromUri] string port, [FromUri] string wsInterface, [FromUri] string resource)
		{
			using (var client = new HttpClient())    
			{ 
				HttpResponseMessage response;
				client.BaseAddress = new Uri("http://"+host+":"+port+"/");    
				var r = client.PutAsync(wsInterface+"/"+resource,null).Result;
				Console.Write (r.StatusCode.ToString());
				if (r.IsSuccessStatusCode) {
					response = new HttpResponseMessage (HttpStatusCode.OK);
				} else
					response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
				response.Headers.Add("Access-Control-Allow-Origin", "*");
				response.Headers.Add("Access-Control-Allow-Methods", "PUT");
				return response;
			}  
		}

		[HttpPut]
		[AcceptVerbs("OPTIONS")]
		public HttpResponseMessage PutParam([FromUri] string host, [FromUri] string port, [FromUri] string wsInterface, [FromUri] string resource, [FromUri] string param)
		{
			using (var client = new HttpClient())    
			{ 
				HttpResponseMessage response;
				client.BaseAddress = new Uri("http://"+host+":"+port+"/");    
				var r = client.PutAsync(wsInterface+"/"+resource+"/"+param,null).Result;
				if (r.IsSuccessStatusCode) {
					response = new HttpResponseMessage (HttpStatusCode.OK);
				} else
					response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
				response.Headers.Add("Access-Control-Allow-Origin", "*");
				response.Headers.Add("Access-Control-Allow-Methods", "PUT");
				return response;
			}  
		}
	}
}
