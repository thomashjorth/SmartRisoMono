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
			 * http://127.0.0.1:9001/api/Realtime/
			 * ?
			 * host=localhost
			 * &
			 * port=8080
			 * &
			 * wsInterface=GenericLoadWS
			 * &
			 * resource=ActivePower
			*/
			if (resource == "getActivePower") {
				response = Request.CreateResponse (HttpStatusCode.Created, WS.DownloadXML (wsInterface, resource, host, port, ParseType.CompositeMeasurement));
			} else {
				response = Request.CreateResponse (HttpStatusCode.Created, "NAN1");

			}
			//Double.Parse (Utils.WS.DownloadXML (id,"localhost","8085"))));
			response.Headers.Add("Access-Control-Allow-Origin", "*");
			response.Headers.Add("Access-Control-Allow-Methods", "GET");
			return response;
		}

		// POST: api/Realtime
		public void Post([FromBody]string value)
		{
		}

		// PUT: api/Realtime/5
		public void Put(int id, [FromBody]string value)
		{
		}
	}
}
