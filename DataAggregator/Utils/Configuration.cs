using System;
using DataAggregator.Models;
using System.Collections.Generic;
namespace DataAggregator.Utils
{
	public static class Configuration
	{
		public static List<DER> DerConfig(){
		
			DER der = new DER();
			der.hostname = "localhost";
			der.port = "8085";
			der.role = "consumer";
			der.type = "house";

			DER der2 = new DER();
			der2.hostname = "localhost";
			der2.port = "8086";
			der2.role = "consumer";
			der2.type = "washingmachine";

			DER der3 = new DER();
			der3.hostname = "localhost";
			der3.port = "8087";
			der3.role = "producer";
			der3.type = "windmill";

			List<DER> ders = new List<DER>{ 
				der, 
				der2 ,
				der3
			};

			return ders;
		}
	}
}

