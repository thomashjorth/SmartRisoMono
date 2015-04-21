using System;
using System.Collections.Generic;
using DataAggregator.Models;
using DataAggregator.Utils;
using System.IO;
using Newtonsoft.Json;
using DataModel;

namespace DataAggregator.Models
{
	static class SimpleStatistics
	{
		public static CompositeMeasurement AvgActivePower(List<DER> ders){
			
			double sum = 0.0;
			string value;
			int count = 0;
			long lastTimestamp = 0;
			foreach (DER d in ders) {
				
				value = WS.DownloadXML ("GenericLoadWS","getActivePower", 
					d.Hostname, d.Port,
					ParseType.CompositeMeasurement);
				if(!value.Equals("NAN")){
					using (var sr = new StringReader(value))
					using (var jr = new JsonTextReader(sr))
					{
						var js = new JsonSerializer();
						var composite = js.Deserialize<CompositeMeasurement>(jr);
						sum += composite.value;
						lastTimestamp = composite.timestampMicros;
						count++;
					}

				}

			}
			CompositeMeasurement returnVal = new CompositeMeasurement ();
			returnVal.value = sum / count;
			returnVal.timestampMicros = lastTimestamp;

			return returnVal;
		}



		public static List<LabeledInstance> AllActivePower(List<DER> ders, int f){

			List<LabeledInstance> res = new List<LabeledInstance> ();
			string value;
			//Random rnd = new Random();

			foreach (DER d in ders) {

				value = WS.DownloadXML (d.WsInterface, "getActivePower", d.Hostname, d.Port,ParseType.CompositeMeasurement);
				System.Diagnostics.Debug.Write (value);
				if(!value.Equals("NAN")){
					using (var sr = new StringReader(value))
					using (var jr = new JsonTextReader(sr))
					{
						var js = new JsonSerializer();
						var composite = js.Deserialize<CompositeMeasurement>(jr);
						if (f == 1) {
							if (composite.value > 0) {
								res.Add(	new LabeledInstance(
									d.Hostname+" : " + d.Port , 
									composite.value));
							}

						}else if(f == -1){
							if (composite.value < 0) {
								res.Add(	new LabeledInstance(
									d.Hostname+" : " + d.Port , 
									composite.value*-1));
							}
						}


						//	rnd.Next(1, 13)));
						System.Diagnostics.Debug.Write (res.Count);
					}
				}
			}
			return res;
		}


	}

}

