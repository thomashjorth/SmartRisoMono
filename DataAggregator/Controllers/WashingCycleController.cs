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


		public HttpResponseMessage Post([FromUri] string setting,[FromUri] string val)
		{
			try
			{
				string[] Lines = File.ReadAllLines(homePath+"/DataAggregatorData/WashingMachine/settings.csv");
				if(setting.Equals("numberOfPrograms")){
					Lines[0]="numberOfPrograms;"+val;
				}
				File.Delete (homePath+"/DataAggregatorData/WashingMachine/settings.csv");
				string settingFile ="";
				foreach(string s in Lines){
					settingFile+=s+"\n";
				}
				File.AppendAllText (homePath+"/DataAggregatorData/WashingMachine/settings.csv", settingFile);


				var response = new HttpResponseMessage(HttpStatusCode.NoContent);
				response.Headers.Location = new Uri(Request.RequestUri.ToString());
				return response;
			}
			catch (Exception e)
			{
				var response = new HttpResponseMessage(HttpStatusCode.Conflict);
				response.Content = new StringContent(e.Message);
				throw new HttpResponseException(response);                
			}            
		}

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
				read = file.ReadToEnd();

					response = Request.CreateResponse (
						HttpStatusCode.Created, 
						read

					);

			} catch(Exception e) {
				read = e.ToString ();
				}
			
			response = Request.CreateResponse (
				HttpStatusCode.Created, 
					read
				);

			response.Headers.Add ("Access-Control-Allow-Origin", "*");
			response.Headers.Add ("Access-Control-Allow-Methods", "GET");

			return response;
		}
    }
}
