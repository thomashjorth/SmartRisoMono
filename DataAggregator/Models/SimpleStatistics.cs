using System;
using System.Collections.Generic;
using DataAggregator.Models;
using DataAggregator.Utils;

namespace DataAggregator.Models
{
	static class SimpleStatistics
	{
		public static double AvgActivePower(List<DER> ders){
			double sum = 0.0;
			string value;
			int count = 0;
			foreach (DER d in ders) {
				
				value = Utils.WS.DownloadXML ("getActivePower", d.hostname, d.port).Replace('.',',');
				System.Diagnostics.Debug.WriteLine ("parse: " + Double.Parse(value));
				if(!value.Equals("NAN")){
					sum += Double.Parse(value);
					System.Diagnostics.Debug.WriteLine ("sum: "  + sum);
					count++;
				}

			}
			System.Diagnostics.Debug.WriteLine (sum + " " + Math.Round (sum / count, 2));
			return sum/count;
		}
	}
}

