using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAggregator.Utils;

namespace DataAggregator.Controllers
{
	public class RealtimeController : ApiController
	{
		// GET: api/Realtime/5
		public HttpResponseMessage Get(
			[FromUri] string host, 
			[FromUri] string port, 
			[FromUri] string wsInterface, 
			[FromUri] string resource)
		{
			HttpResponseMessage response;
			/* Example use
			 * http://127.0.0.1:9001/api/Realtime/?host=localhost&port=8080&wsInterface=GenericLoadWS&resource=getActivePower
			*/

			string data = WS.GetCompositeMeasurement (wsInterface, resource, host, port);
			response = Request.CreateResponse(HttpStatusCode.Created,data);
			//Double.Parse (Utils.WS.DownloadXML (id,"localhost","8085"))));
			response.Headers.Add("Access-Control-Allow-Origin", "*");
			response.Headers.Add("Access-Control-Allow-Methods", "GET");
			return response;
		}

		[AllowAnonymous]
		[AcceptVerbs("OPTIONS")]
		public HttpResponseMessage Put(
			[FromUri] string host, 
			[FromUri] string port, 
			[FromUri] string wsInterface, 
			[FromUri] string resource)
		{
			using (var client = new HttpClient())    
			{ 
				client.BaseAddress = new Uri("http://"+host+":"+port+"/");    
				var r = client.PutAsync(wsInterface+"/"+resource,null).Result;
				Console.Write (r.StatusCode.ToString());
				if (r.IsSuccessStatusCode) {
					return new HttpResponseMessage(HttpStatusCode.OK);
				} else
					return new HttpResponseMessage(HttpStatusCode.InternalServerError);
			}  
		}
	}
}
