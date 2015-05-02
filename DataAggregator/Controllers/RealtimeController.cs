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
		

			if (resource == GenericLoadWSGET.getActivePower.ToString()) {
				response = Request.CreateResponse (HttpStatusCode.Created, WS.DownloadXML (wsInterface, resource, host, port, 
					ParseType.CompositeMeasurement));
			}else if (resource == GenericPriceWSGET.getCurrentPrice.ToString()) {
				response = Request.CreateResponse (HttpStatusCode.Created, WS.DownloadXML (wsInterface, resource, host, port, 
					ParseType.CompositeMeasurement));
			}else if (resource == GenericPriceWSGET.getDailyPriceAverage.ToString()) {
				response = Request.CreateResponse (HttpStatusCode.Created, WS.DownloadXML (wsInterface, resource, host, port, 
					ParseType.CompositeMeasurement));
			}else if (resource == GenericPriceWSGET.getEnergyUnit.ToString()) {
				response = Request.CreateResponse (HttpStatusCode.Created, WS.DownloadXML (wsInterface, resource, host, port, 
					ParseType.String));
			}else if (resource == GenericPriceWSGET.getMonetaryUnit.ToString()) {
				response = Request.CreateResponse (HttpStatusCode.Created, WS.DownloadXML (wsInterface, resource, host, port, 
					ParseType.String));
			}else if (resource == GenericPriceWSGET.getScriptName.ToString()) {
				response = Request.CreateResponse (HttpStatusCode.Created, WS.DownloadXML (wsInterface, resource, host, port, 
					ParseType.String));
			}else if (resource == GenericPriceWSGET.getUpdateInterval.ToString()) {
				response = Request.CreateResponse (HttpStatusCode.Created, WS.DownloadXML (wsInterface, resource, host, port, 
					ParseType.Double));
			}
			else {
				response = Request.CreateResponse (HttpStatusCode.Created, "Data Aggregator: Non supported resource");

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
		public void Put(
			[FromUri] string host, 
			[FromUri] string port, 
			[FromUri] string wsInterface, 
			[FromUri] string resource, 
			[FromBody]string value)
		{

		}
	}
}
