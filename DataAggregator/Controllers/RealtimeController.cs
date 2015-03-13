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
		public HttpResponseMessage Get(string id)
		{
			var response = Request.CreateResponse (HttpStatusCode.Created, Utils.JSONUtil.ToJSON(

			Double.Parse (Utils.WS.DownloadXML (id,"localhost","8085"))));
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
