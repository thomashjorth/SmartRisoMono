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
			List<DER> ders = Utils.Configuration.DerConfig ();

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