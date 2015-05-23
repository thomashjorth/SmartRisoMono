using System;
using System.Net;
using System.Xml.Linq;
using DataModel.Syslab;
using System.Globalization;
using System.Collections.Generic;


namespace DataAggregator.Utils
{

	public enum GenericPriceWSGET { getCurrentPrice,getDailyPriceAverage,getEnergyUnit,getMonetaryUnit,getScriptName,getUpdateInterval };

	public enum ParseType{
		CompositeMeasurement, String, Double
	}


	public static class WS
	{ 

		public static string GetCompositeMeasurement(string Interface, string function, string hostname, string port){
			var compositeM = convertXMLToCompositeMeasurement (Interface, function, hostname, port);
			if (compositeM == null)
				return "NAN";
			return Newtonsoft.Json.JsonConvert.SerializeObject (compositeM);
		}

		public static string GetCompositeMeasurementList(string Interface, string function, string hostname, string port){
			var compositeM = convertXMLToCompositeMeasurementList (Interface, function, hostname, port)[0];
			if (compositeM == null)
				return "NAN";
			return Newtonsoft.Json.JsonConvert.SerializeObject (compositeM);
		}

		public static string GetCompositeBoolean(string Interface, string function, string hostname, string port){
			var compositeB = convertXMLToCompositeBoolean (Interface, function, hostname, port);
			if (compositeB == null)
				return "NAN";
			return Newtonsoft.Json.JsonConvert.SerializeObject (compositeB);
		}

		public static string GetStatus(string Interface, string function, string hostname, string port){
			var status = convertXMLToStatus (Interface, function, hostname, port);
			if (status == null)
				return "NAN";
			return Newtonsoft.Json.JsonConvert.SerializeObject (status);
		}

		public static CompositeMeasurement convertXMLToCompositeMeasurement(string Interface, string function, string hostname, string port){
			string xml = GetData(Interface,function,hostname,port);
			if(xml == "NAN")
				return null;
			return new CompositeMeasurement(xml);
		}

		public static List<CompositeMeasurement> convertXMLToCompositeMeasurementList(string Interface, string function, string hostname, string port){
			string xml = GetData(Interface,function,hostname,port);
			if(xml == "NAN")
				return null;
			XDocument document = XDocument.Parse(xml);
			List<CompositeMeasurement> cmList = new List<CompositeMeasurement> ();
			foreach(XElement element in document.Root.Elements()){
				cmList.Add (new CompositeMeasurement(element.ToString()));
			}
			return cmList;
		}

		public static CompositeBoolean convertXMLToCompositeBoolean(string Interface, string function, string hostname, string port){
			string xml = GetData(Interface,function,hostname,port);
			if(xml == "NAN")
				return null;
			return new CompositeBoolean(xml);
		}

		public static Status convertXMLToStatus(string Interface, string function, string hostname, string port){
			string xml = GetData(Interface,function,hostname,port);
			if(xml == "NAN")
				return null;
			return new Status(xml);
		}

		public static string GetString(string Interface, string function, string hostname, string port){
			XDocument doc = XDocument.Parse(GetData(Interface,function,hostname,port));
			string xmlString =doc.Element("string").Value;
			return Newtonsoft.Json.JsonConvert.SerializeObject (xmlString);
		}

		public static string GetDouble(string Interface, string function, string hostname, string port){
			XDocument doc = XDocument.Parse(GetData(Interface,function,hostname,port));
			double xmlDouble =double.Parse(doc.Element("double").Value);
			return Newtonsoft.Json.JsonConvert.SerializeObject (xmlDouble);
		}

		public static string GetData(string Interface, string function, string hostname, string port){
			string url = "http://" + hostname + ":" + port + "/" + Interface + "/" + function;
			string xml;
			using (var webClient = new WebClient())
			{try{
					xml = webClient.DownloadString(url);
				}
				catch{
					return "NAN";
				}
			}
			return xml;
		}

	}
}

