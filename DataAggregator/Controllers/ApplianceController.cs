using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using DataModel;
using DataModel.Syslab;
using System.IO;

namespace DataAggregator.Controllers
{
    public class ApplianceController : ApiController
    {
		string homePath = (Environment.OSVersion.Platform == PlatformID.Unix || 
			Environment.OSVersion.Platform == PlatformID.MacOSX)
			? Environment.GetEnvironmentVariable("HOME")
			: Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");


		// GET: hostname:port/api/Aggregation/id
		public HttpResponseMessage Get([FromUri] string id, [FromUri] string attribute)
		{
			string openFile = "";
			string read = "";
			List<LabeledMeasurement> programEnergy = new List<LabeledMeasurement>(){};
			try {
				StreamReader file = File.OpenText (homePath + "/DataAggregatorData/WashingMachine/centroidsEnergy.json");
				read = file.ReadToEnd();
				programEnergy = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LabeledMeasurement>>(read);
			
			} catch(Exception e) {
				read = "FEJL1"+e.ToString ();
			}

			List<LabeledMeasurement> programCount = new List<LabeledMeasurement>(){};
			try {
				StreamReader file = File.OpenText (homePath + "/DataAggregatorData/WashingMachine/programsCount.json");
				read = file.ReadToEnd();
				programCount = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LabeledMeasurement>>(read);

			} catch(Exception e) {
				read = "FEJL1"+e.ToString ();
			}

			EEI eeiEU = new EEI (7, new double[]{ programEnergy [0].measurement.value, programEnergy [1].measurement.value, programEnergy [2].measurement.value });
			EEI eeiLOW = new EEI (7, new double[]{ programEnergy [0].measurement.value, programEnergy [1].measurement.value, programEnergy [2].measurement.value },1,0,0);
			EEI eeiMIDDLE = new EEI (7, new double[]{ programEnergy [0].measurement.value, programEnergy [1].measurement.value, programEnergy [2].measurement.value },0,1,0);
			EEI eeiHIGH = new EEI (7, new double[]{ programEnergy [0].measurement.value, programEnergy [1].measurement.value, programEnergy [2].measurement.value },0,0,1);
			EEI eeiYou = new EEI (7, new double[]{ programEnergy [0].measurement.value, programEnergy [1].measurement.value, programEnergy [2].measurement.value },
				(int)programCount[0].measurement.value,
				(int)programCount[1].measurement.value,
				(int)programCount[2].measurement.value);

			HttpResponseMessage response;

			if (id.Equals ("Rating")) {
				string Rating = ""; 

				if (attribute.Equals ("LOW")) {
					Rating = eeiLOW.Rating ();
				} else if (attribute.Equals ("MIDDLE")) {
					Rating = eeiMIDDLE.Rating ();
				} else if (attribute.Equals ("HIGH")) {
					Rating = eeiHIGH.Rating ();
				} else {
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
			} else if (id.Equals ("Score")) {
				
				CompositeMeasurement Score;

				if (attribute.Equals ("LOW")) {
					Score = new CompositeMeasurement (eeiLOW.EeiScore ());
				} else if (attribute.Equals ("MIDDLE")) {
					Score = new CompositeMeasurement (eeiMIDDLE.EeiScore ());
				} else if (attribute.Equals ("HIGH")) {
					Score = new CompositeMeasurement (eeiHIGH.EeiScore ());
				} else if (attribute.Equals ("ALL")) {
					List<LabeledMeasurement> allScore = new List<LabeledMeasurement> (){ };
					allScore.Add (new LabeledMeasurement ("LOW: " + eeiLOW.Rating (), new CompositeMeasurement (eeiLOW.EeiScore ())));
						allScore.Add (new LabeledMeasurement ("MIDDLE: " + eeiMIDDLE.Rating (), new CompositeMeasurement (eeiMIDDLE.EeiScore ())));
						allScore.Add (new LabeledMeasurement ("HIGH: " + eeiHIGH.Rating (), new CompositeMeasurement (eeiHIGH.EeiScore ())));
						allScore.Add (new LabeledMeasurement ("EU: " + eeiEU.Rating (), new CompositeMeasurement (eeiEU.EeiScore ())));
						allScore.Add (new LabeledMeasurement ("You: " + eeiYou.Rating (), new CompositeMeasurement (eeiYou.EeiScore ())));

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

				return response;
			} else if (id.Equals ("AEC")) {

				CompositeMeasurement AEC;

				if (attribute.Equals ("LOW")) {
					AEC = new CompositeMeasurement (eeiLOW.EeiScore ());
				} else if (attribute.Equals ("MIDDLE")) {
					AEC = new CompositeMeasurement (eeiMIDDLE.EeiScore ());
				} else if (attribute.Equals ("HIGH")) {
					AEC = new CompositeMeasurement (eeiHIGH.EeiScore ());
				} else if (attribute.Equals ("ALL")) {
					List<LabeledMeasurement> allAec = new List<LabeledMeasurement> (){ };
					allAec.Add (new LabeledMeasurement ("LOW: " + eeiLOW.Rating (), new CompositeMeasurement (eeiLOW.AEC ())));
					allAec.Add (new LabeledMeasurement ("MIDDLE: " + eeiMIDDLE.Rating (), new CompositeMeasurement (eeiMIDDLE.AEC ())));
					allAec.Add (new LabeledMeasurement ("HIGH: " + eeiHIGH.Rating (), new CompositeMeasurement (eeiHIGH.AEC ())));
					allAec.Add (new LabeledMeasurement ("EU: " + eeiEU.Rating (), new CompositeMeasurement (eeiEU.AEC ())));
					allAec.Add (new LabeledMeasurement ("You: " + eeiYou.Rating (), new CompositeMeasurement (eeiYou.AEC ())));

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
			} else if (id.Equals ("Program")) {
				if (attribute.Equals ("Count")) {
					openFile = "/DataAggregatorData/WashingMachine/programsCount.json";
				} else if (attribute.Equals ("PowerCentroid")) {
					openFile = "/DataAggregatorData/WashingMachine/centroidsPower.json";
				} else if (attribute.Equals ("EnergyCentroid")) {
					openFile = "/DataAggregatorData/WashingMachine/centroidsEnergy.json";
				} else if (attribute.Equals ("Discovered")) {
					openFile = "/DataAggregatorData/WashingMachine/discoveredCycles.json";
				}
				read = "";

				try {
					StreamReader file = File.OpenText (homePath + openFile);
					read = file.ReadToEnd ();

				} catch (Exception e) {
					read = "NAN";
				}

				response = Request.CreateResponse (
					HttpStatusCode.Created, 
					read
				);

				response = Request.CreateResponse (
					HttpStatusCode.Created, 
					read

				);
				response.Headers.Add ("Access-Control-Allow-Origin", "*");
				response.Headers.Add ("Access-Control-Allow-Methods", "GET");

				return response;
			}


			response = Request.CreateResponse (
				HttpStatusCode.Created, 
				"NAN"
			);
			response.Headers.Add ("Access-Control-Allow-Origin", "*");
			response.Headers.Add ("Access-Control-Allow-Methods", "GET");
			return response;
			}
    }
}
