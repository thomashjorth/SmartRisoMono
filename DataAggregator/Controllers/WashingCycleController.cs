using System;
using System.Web.Http;
using System.Net.Http;
using System.IO;
using System.Net;

namespace DataAggregator.Controllers
{
    public class WashingCycleController : ApiController
	{
		string homePath = (Environment.OSVersion.Platform == PlatformID.Unix || 
		Environment.OSVersion.Platform == PlatformID.MacOSX)
			? Environment.GetEnvironmentVariable("HOME")
			: Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
		
		public HttpResponseMessage Get(string id)
		{
			string openFile = "";
			if (id.Equals ("Count")) {
				openFile = "/DataAggregatorData/WashingMachine/programsCount.json";
			} else if (id.Equals ("PowerCentroid")) {
				openFile = "/DataAggregatorData/WashingMachine/centroidsPower.json";
			}else if (id.Equals ("EnergyCentroid")) {
				openFile = "/DataAggregatorData/WashingMachine/centroidsEnergy.json";
			}
			
			HttpResponseMessage response;

			string read = "";

				try {
				StreamReader file = File.OpenText (homePath + openFile);
					read = file.ReadToEnd ();
					read = read.Replace ("\\", "");
					read = read.Remove (0, 1);
					read = read.Remove (read.Length - 1, 1);

					response = Request.CreateResponse (
						HttpStatusCode.Created, 
						read

					);

				} catch {
				}
			
			response = Request.CreateResponse (
				HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject (
					read
				));

			response.Headers.Add ("Access-Control-Allow-Origin", "*");
			response.Headers.Add ("Access-Control-Allow-Methods", "GET");

			return response;
		}
    }
}
