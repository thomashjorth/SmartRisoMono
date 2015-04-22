using System;
using DataAggregator.Models;
using System.Collections.Generic;
namespace DataAggregator.Utils
{
	public static class Configuration
	{
		public static List<DER> DerConfig(bool simulatedTest){
			List<DER> ders;
			if (simulatedTest) {
				DER der = new DER ("localhost", "8080");
				DER der2 = new DER ("localhost", "8085");
				DER der3 = new DER ("localhost", "8086");
				ders = new List<DER> { 
					der, der2,der3
				};
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
