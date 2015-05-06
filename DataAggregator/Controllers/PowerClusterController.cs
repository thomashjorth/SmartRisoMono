using System;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using DataAggregator.Models;
using System.IO;

namespace DataAggregator.Controllers
{
    public class PowerClusterController : ApiController
    {
		public HttpResponseMessage Get(
			[FromUri] int nClusters, 
			[FromUri] string host )
		{
			string homePath = (Environment.OSVersion.Platform == PlatformID.Unix || 
				Environment.OSVersion.Platform == PlatformID.MacOSX)
				? Environment.GetEnvironmentVariable("HOME")
				: Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
			HttpResponseMessage response;

			DER der = new DER (host, "8080");
			string res = "";
			string[] dirs;
			try 
			{
				// Only get files that begin with the letter "c." 
				dirs = Directory.GetFiles(homePath+"/DataAggregatorData/WashingMachine/Power/","*.csv");

				foreach (string dir in dirs) 
				{
					res= res+ " "+ dir;
				}
			} 
			catch (Exception e) 
			{
				res = res + " " + e.ToString();
			}

			foreach (string dir in dirs) 
			{
				
			}

			response = Request.CreateResponse (
				HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject (
					res
				));

			response.Headers.Add ("Access-Control-Allow-Origin", "*");
			response.Headers.Add ("Access-Control-Allow-Methods", "GET");

			return response;
		}
    }
}
