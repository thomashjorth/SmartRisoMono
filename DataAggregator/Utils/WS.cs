using System;
using System.Net;
using System.Xml.Linq;
using DataModel;
using System.Globalization;
namespace DataAggregator.Utils
{

	public enum GenericLoadWSGET { getNodeConfiguration, getActiveOperatingMode, getActivePSchedules, 
		getActivePower, getActiveQSchedules, getAvailableOperatingModes, getAvailableSchedules, 
		getConstantP, getConstantQ, getFrequency, getGPSLocation, getInactivePValue, 
		getInactiveQValue, getInstantaneous_P_U_Droop, getInstantaneous_P_f_Droop, 
		getInstantaneous_Q_U_Droop, getInstantaneous_Q_f_Droop, getInstantaneous_U0, 
		getInstantaneous_f0, getInterphaseVoltages, getLoadHealth, getLoadLogicalNameplate, 
		getLoadName, getLoadPhysicalNameplate, getPAutocorrelation, getPMean, getPResponseTime, 
		getPStdDeviation, getPStep, getP_U_Characteristics, getP_f_Characteristics, getPhaseCurrents, 
		getQAutocorrelation, getQMean, getQResponseTime, getQStdDeviation, getQStep, 
		getQ_U_Characteristics, getQ_f_Characteristics, getRatedP, getRatedQ, getReactivePower, 
		isPControllable, isPStepped, isSchedulePaused, isQControllable, isQStepped, isReactiveLoad, 
		isResistiveLoad, isScheduleExecuting, hasFFeedback, hasPFeedback, hasQFeedback, hasUFeedback, 
		isLoadOn, hasFan, isFanRunning, getTemperature, hasTemperatureMeasurement };

	public enum GenericPriceWSGET { getCurrentPrice,getDailyPriceAverage,getEnergyUnit,getMonetaryUnit,getScriptName,getUpdateInterval };

	public enum ParseType{
		CompositeMeasurement, String, Double
	}


	public static class WS
	{ 

		public static string GetCompositeMeasurement(string Interface, string function, string hostname, string port){
			string xml = GetData(Interface,function,hostname,port);
			if(xml == "NAN")
				return xml;
			XDocument doc = XDocument.Parse(xml);

			CompositeMeasurement activePower = new CompositeMeasurement ();			
			activePower.value 			= Math.Round(Double.Parse(
				doc.Root.Element("value").Value.Replace(',', '.'), 
				CultureInfo.InvariantCulture
			),2);
			System.Diagnostics.Debug.Write (Double.Parse(doc.Root.Element	("value").Value.Substring (0, 4)));
			activePower.timestampMicros = long.Parse(doc.Root.Element	("timestampMicros").Value);
			activePower.timePrecision 	= short.Parse(doc.Root.Element	("timePrecision").Value);
			activePower.quality 		= byte.Parse(doc.Root.Element	("quality").Value);
			activePower.validity 		= byte.Parse(doc.Root.Element	("validity").Value);
			activePower.source 			= byte.Parse(doc.Root.Element	("source").Value);
			return Newtonsoft.Json.JsonConvert.SerializeObject (activePower);

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

