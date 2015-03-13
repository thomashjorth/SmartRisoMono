using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using DataAggregator.Models;
using System.IO;
using System.Runtime.Serialization.Json;

namespace DataAggregator.Controllers
{
    public class AggregationController : ApiController
    {
		List<string> functions = new List<string> { 
			"DeviceList", "AvgActivePower"
		};
		
		// GET: hostname:port/api/Aggregation/id
		public string Get(string id)
		{
			//NetworkAggregation netAgg = new NetworkAggregation("~/../DERs.json");
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

			if (id == functions.ElementAt (0)) {
				return Utils.JSONUtil.ToJSON(ders);
			} 
			if (id == functions.ElementAt (1)) {
				
				return ""+SimpleStatistics.AvgActivePower(ders);
			} 


			// Wrong id:
			else {
				string output= "";
				foreach (string s in functions) {
					output = output + " : " + s;
				}
				return id + " : is not a function supported by this Web Service. Try: " + output;
			}
		//	NetworkAggregation netA = new NetworkAggregation ("../DERs.xml");

		}
	}}