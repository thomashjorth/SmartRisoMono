﻿using System;
using System.Collections.Generic;
using DataAggregator.Utils;
using System.IO;
using Newtonsoft.Json;
using DataModel;
using DataModel.Syslab;

namespace DataAggregator.Utils
{
	static class SimpleStatistics
	{
		public static CompositeMeasurement AvgActivePower(List<DER> ders){
			
			double sum = 0.0;
			string value;
			int count = 0;
			long lastTimestamp = 0;
			foreach (DER d in ders) {
				
				value = WS.GetCompositeMeasurement (d.Interface+"WS","getActivePower", 
					d.Hostname, d.Port);
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



		public static List<LabeledInstance> AllActivePower(List<DER> ders){

			List<LabeledInstance> res = new List<LabeledInstance> ();
			string value;
			//Random rnd = new Random();

			foreach (DER d in ders) {

				value = WS.GetCompositeMeasurement (d.Interface+"WS", "getActivePower", d.Hostname, d.Port);
				System.Diagnostics.Debug.Write (value);
				if (!value.Equals ("NAN")) {
					using (var sr = new StringReader (value))
					using (var jr = new JsonTextReader (sr)) {
						var js = new JsonSerializer ();
						var composite = js.Deserialize<CompositeMeasurement> (jr);
						res.Add (new LabeledInstance (
							d.Hostname + " : " + d.Port, 
							composite.value));
			
							
					}


					//	rnd.Next(1, 13)));
					System.Diagnostics.Debug.Write (res.Count);
				

				}

			}
			return res;
	}

	}}

