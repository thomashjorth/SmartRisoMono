using System.Web.Http;
using System.Net.Http;
using System.Net;
using DataModel;

namespace DataAggregator.Controllers
{
    public class DERController : ApiController
    {
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
    }
}
