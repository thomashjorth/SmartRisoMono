﻿using System;
using System.Net;
using System.Xml.Linq;
using DataModel;
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
			var compositeM = convertXMLToCompositeBoolean (Interface, function, hostname, port);
			if (compositeM == null)
				return "NAN";
			return Newtonsoft.Json.JsonConvert.SerializeObject (compositeM);
		}

		public static CompositeMeasurement convertXMLToCompositeMeasurement(string Interface, string function, string hostname, string port){
			string xml = GetData(Interface,function,hostname,port);
			if(xml == "NAN")
				return null;
			XDocument doc = XDocument.Parse(xml);

			CompositeMeasurement activePower = new CompositeMeasurement ();
			if (doc.Root.Element ("value").Value == "0.0")
				activePower.value = 0;
			else {
				activePower.value = Math.Round (Double.Parse (
					doc.Root.Element ("value").Value.Replace (',', '.'), 
					CultureInfo.InvariantCulture
				), 2);
			}
			activePower.timestampMicros = long.Parse(doc.Root.Element	("timestampMicros").Value);
			activePower.timePrecision 	= short.Parse(doc.Root.Element	("timePrecision").Value);
			activePower.quality 		= byte.Parse(doc.Root.Element	("quality").Value);
			activePower.validity 		= byte.Parse(doc.Root.Element	("validity").Value);
			activePower.source 			= byte.Parse(doc.Root.Element	("source").Value);
			return activePower;
		}

		public static List<CompositeMeasurement> convertXMLToCompositeMeasurementList(string Interface, string function, string hostname, string port){
			string xml = GetData(Interface,function,hostname,port);
			if(xml == "NAN")
				return null;
			XDocument document = XDocument.Parse(xml);
			List<CompositeMeasurement> cmList = new List<CompositeMeasurement> ();
			foreach(XElement element in document.Root.Elements()){
				var doc = XDocument.Parse(element.ToString ());
				System.Diagnostics.Debug.WriteLine (element.Document.ToString());
				CompositeMeasurement cm = new CompositeMeasurement ();
				cm.value 			= Math.Round(Double.Parse(
					doc.Root.Element("value").Value.Replace(',', '.'), 
					CultureInfo.InvariantCulture
				),3);
				System.Diagnostics.Debug.Write (Double.Parse(doc.Root.Element	("value").Value.Substring (0, 4)));
				cm.timestampMicros = long.Parse(doc.Root.Element	("timestampMicros").Value);
				cm.timePrecision 	= short.Parse(doc.Root.Element	("timePrecision").Value);
				cm.quality 		= byte.Parse(doc.Root.Element	("quality").Value);
				cm.validity 		= byte.Parse(doc.Root.Element	("validity").Value);
				cm.source 			= byte.Parse(doc.Root.Element	("source").Value);
				cmList.Add (cm);
			}
			return cmList;
		}

		public static CompositeBoolean convertXMLToCompositeBoolean(string Interface, string function, string hostname, string port){
			string xml = GetData(Interface,function,hostname,port);
			if(xml == "NAN")
				return null;
			XDocument doc = XDocument.Parse(xml);

			CompositeBoolean activePower = new CompositeBoolean ();			
			activePower.value 			= doc.Root.Element("value").Value == "true";
			activePower.timestampMicros = long.Parse(doc.Root.Element	("timestampMicros").Value);
			activePower.timePrecision 	= short.Parse(doc.Root.Element	("timePrecision").Value);
			activePower.quality 		= byte.Parse(doc.Root.Element	("quality").Value);
			activePower.validity 		= byte.Parse(doc.Root.Element	("validity").Value);
			activePower.source 			= byte.Parse(doc.Root.Element	("source").Value);
			return activePower;
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

		private static string GetData(string Interface, string function, string hostname, string port){
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

