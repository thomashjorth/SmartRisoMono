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
				
				value = WS.DownloadXML ("getActivePower", d.hostname, d.port);
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

				value = WS.DownloadXML ("getActivePower", d.hostname, d.port);
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
									d.hostname+" : " + d.port , 
									composite.value));
							}

						}else if(f == -1){
							if (composite.value < 0) {
								res.Add(	new LabeledInstance(
									d.hostname+" : " + d.port , 
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

	public class LabeledInstance{
		public LabeledInstance(string l, double i){
			label = l;
			instances = i;
		}
		public string label;
		public double instances;
	}
}

