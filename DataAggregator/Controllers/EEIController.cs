using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using DataAggregator.Models;
using DataModel;
using System.IO;

namespace DataAggregator.Controllers
{
    public class EEIController : ApiController
    {
		string homePath = (Environment.OSVersion.Platform == PlatformID.Unix || 
			Environment.OSVersion.Platform == PlatformID.MacOSX)
			? Environment.GetEnvironmentVariable("HOME")
			: Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");


		// GET: hostname:port/api/Aggregation/id
		public HttpResponseMessage Get([FromUri] string id, [FromUri] string situation)
		{
			string read = "";
			List<LabeledInstance> programEnergy = new List<LabeledInstance>(){};
			try {
				StreamReader file = File.OpenText (homePath + "/DataAggregatorData/WashingMachine/centroidsEnergy.json");
				read = file.ReadToEnd();
				programEnergy = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LabeledInstance>>(read);
			
			} catch(Exception e) {
				read = e.ToString ();
			}
			EEI eeiEU = new EEI (7, new double[]{ programEnergy [1].value, programEnergy [2].value, programEnergy [3].value });
			EEI eeiLOW = new EEI (7, new double[]{ programEnergy [1].value, programEnergy [1].value, programEnergy [1].value });
			EEI eeiMIDDLE = new EEI (7, new double[]{ programEnergy [2].value, programEnergy [2].value, programEnergy [2].value });
			EEI eeiHIGH = new EEI (7, new double[]{ programEnergy [3].value, programEnergy [3].value, programEnergy [3].value });

			HttpResponseMessage response;
			if (id.Equals ("Rating")) {
				string Rating = ""; 

				if (situation.Equals ("LOW")) {
					Rating = eeiLOW.Rating ();
				} else if (situation.Equals ("MIDDLE")) {
					Rating = eeiMIDDLE.Rating ();
				} else if (situation.Equals ("HIGH")) {
					Rating = eeiHIGH.Rating ();
				}
				else {
					Rating = eeiEU.Rating ();
				}
				response = Request.CreateResponse (
					HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject (
						Rating
					)
				);
				response.Headers.Add ("Access-Control-Allow-Origin", "*");
				response.Headers.Add ("Access-Control-Allow-Methods", "GET");

				return response;
			}else if (id.Equals ("Score")) {
				
				CompositeMeasurement Score;

				if (situation.Equals ("LOW")) {
					Score = new CompositeMeasurement (eeiLOW.EeiScore ());
				} else if (situation.Equals ("MIDDLE")) {
					Score = new CompositeMeasurement (eeiMIDDLE.EeiScore ());
				} else if (situation.Equals ("HIGH")) {
					Score = new CompositeMeasurement (eeiHIGH.EeiScore ());
				} else if (situation.Equals ("ALL")) {
					List<LabeledInstance> allScore = new List<LabeledInstance> (){ };
					allScore.Add (new LabeledInstance ("LOW: " + eeiLOW.Rating(), eeiLOW.EeiScore ()));
					allScore.Add (new LabeledInstance ("MIDDLE: " + eeiMIDDLE.Rating(), eeiMIDDLE.EeiScore ()));
					allScore.Add (new LabeledInstance ("HIGH: " + eeiHIGH.Rating(), eeiHIGH.EeiScore ()));
					allScore.Add (new LabeledInstance ("EU: " + eeiEU.Rating(), eeiEU.EeiScore ()));

					response = Request.CreateResponse (
						HttpStatusCode.Created,
						Newtonsoft.Json.JsonConvert.SerializeObject (allScore)

					);
					response.Headers.Add ("Access-Control-Allow-Origin", "*");
					response.Headers.Add ("Access-Control-Allow-Methods", "GET");
					return response;
				} else {
					Score = new CompositeMeasurement (eeiEU.EeiScore ());
				}
				response = Request.CreateResponse (
					HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject (
						Score
					)
				);
				response.Headers.Add ("Access-Control-Allow-Origin", "*");
				response.Headers.Add ("Access-Control-Allow-Methods", "GET");

				return response;
			}else if (id.Equals ("AEC")) {

				CompositeMeasurement AEC;

				if (situation.Equals ("LOW")) {
					AEC = new CompositeMeasurement (eeiLOW.EeiScore ());
				} else if (situation.Equals ("MIDDLE")) {
					AEC = new CompositeMeasurement (eeiMIDDLE.EeiScore ());
				} else if (situation.Equals ("HIGH")) {
					AEC = new CompositeMeasurement (eeiHIGH.EeiScore ());
				} else if (situation.Equals ("ALL")) {
					List<LabeledInstance> allAec = new List<LabeledInstance> (){ };
					allAec.Add (new LabeledInstance ("LOW: " + eeiLOW.Rating(), eeiLOW.AEC ()));
					allAec.Add (new LabeledInstance ("MIDDLE: " + eeiMIDDLE.Rating(), eeiMIDDLE.AEC ()));
					allAec.Add (new LabeledInstance ("HIGH: " + eeiHIGH.Rating(), eeiHIGH.AEC ()));
					allAec.Add (new LabeledInstance ("EU: " + eeiEU.Rating(), eeiEU.AEC ()));

					response = Request.CreateResponse (
						HttpStatusCode.Created,
							Newtonsoft.Json.JsonConvert.SerializeObject (allAec)

					);
					response.Headers.Add ("Access-Control-Allow-Origin", "*");
					response.Headers.Add ("Access-Control-Allow-Methods", "GET");
					return response;
				} else {
					AEC = new CompositeMeasurement (eeiEU.EeiScore ());
				}
				response = Request.CreateResponse (
					HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject (
						AEC
					)
				);
				response.Headers.Add ("Access-Control-Allow-Origin", "*");
				response.Headers.Add ("Access-Control-Allow-Methods", "GET");

				return response;
			}
					response = Request.CreateResponse (
					HttpStatusCode.Created, Newtonsoft.Json.JsonConvert.SerializeObject (
					"WashingCycles <List<LabeledInstance>>, TotalCycles <CompositeMeasurement>, Rating <String>, Score <CompositeMeasurement>, AEC <CompositeMeasurement>"
					)
				);
				response.Headers.Add ("Access-Control-Allow-Origin", "*");
				response.Headers.Add ("Access-Control-Allow-Methods", "GET");
			return response;

		}
    }
}
