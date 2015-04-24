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
	//public enum GenericLoadWSPUSH { pauseSchedule, setActiveOperatingMode, setActivePSchedule, setActiveQSchedule, setConstantP/{p}, setConstantQ/{q}, setInactivePValue/{d}, setInactiveQValue/{d}, setPAutocorrelation/{a}, setPMean/{m}, setPStdDeviation/{s}, setP_U_Characteristics, setP_f_Characteristics, setQAutocorrelation/{a}, setQMean/{m}, setQStdDeviation/{s}, setQ_U_Characteristics, setQ_f_Characteristics, startSchedule, stopSchedule, startLoad, stopLoad, };

	public enum ParseType{
		CompositeMeasurement, String, Double
	}

	public static class WS
	{ 
		
		private static CompositeMeasurement ParseXmlCompositeMeasurement(string xml){
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
			return activePower;

		}

		private static string ParseXmlString(string xml){
			XDocument doc = XDocument.Parse(xml);
			string xmlString =doc.Element("string").Value;
			return xmlString;
		}

		private static double ParseXmlDouble(string xml){
			XDocument doc = XDocument.Parse(xml);
			double xmlDouble =double.Parse(doc.Element("double").Value);
			return xmlDouble;
		}
		public static string DownloadXML(string Interface, string function, string hostname, string port, ParseType parseAs)
		{
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
			string res = "NAN";
			try{
			if (parseAs == ParseType.CompositeMeasurement) {
				res = Newtonsoft.Json.JsonConvert.SerializeObject (ParseXmlCompositeMeasurement (xml));
			} else if (parseAs == ParseType.String) {
				res = Newtonsoft.Json.JsonConvert.SerializeObject (ParseXmlString (xml));
			}else if (parseAs == ParseType.Double) {
				res = Newtonsoft.Json.JsonConvert.SerializeObject (ParseXmlDouble (xml));
				}
			}catch{
				return res;
			}
			//return doc.Root.Element("value").Value.Substring (0, 4);
			return res;
		}

	}
}

