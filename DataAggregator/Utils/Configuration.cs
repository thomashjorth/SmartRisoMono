using System;
using System.Collections.Generic;
using System.Configuration;
using DataModel;


namespace DataAggregator.Utils
{
	public static class Configuration
	{
		public static List<DER> DerConfig(bool simulatedTest){
			List<DER> ders;
			if (simulatedTest) {
				/*DER der = new DER ("localhost", "8080");
				DER der2 = new DER ("localhost", "8085");
				DER der3 = new DER ("localhost", "8090");
				ders = new List<DER> { 
					der, der2,der3
				};*/
				ders = new List<DER> (){ };
				string dersString;
				dersString = ConfigurationManager.AppSettings["Ders"];

				string[] dersArray = dersString.Split(',');
				List<string> dersList = new List<string> (dersArray);
				foreach(string d in dersList){
					string[] hostPort = d.Split (':');
					ders.Add(new DER(hostPort[0],hostPort[1]));
				}
			} else {

				ders = new List<DER> {
					new DER ("syslab-01", "8080"),
					new DER ("syslab-02", "8080"),
					new DER ("syslab-03", "8080"),
					new DER ("syslab-05", "8080"),
					new DER ("syslab-07", "8080"),
					new DER ("syslab-08", "8080"),
					new DER ("syslab-10", "8080"),
					new DER ("syslab-11", "8080"),
					new DER ("syslab-12", "8080"),
					new DER ("syslab-16", "8080"),
					new DER ("syslab-18", "8080"),
					new DER ("syslab-21", "8080"),
					new DER ("syslab-22", "8080"),
					new DER ("syslab-23", "8080"),
					new DER ("syslab-24", "8080"),
					new DER ("syslab-26", "8080"),
					new DER ("syslab-27", "8080"),
					new DER ("syslab-v06","8080")


				};
				
			}
			return ders;
		}
	}
}
