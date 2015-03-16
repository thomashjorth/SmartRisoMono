using System;
using System.Net;
using System.Xml.Linq;

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
	//public enum GenericLoadWSPUSH { pauseSchedule, setActiveOperatingMode, setActivePSchedule, setActiveQSchedule, setConstantP/{p}, setConstantQ/{q}, setInactivePValue/{d}, setInactiveQValue/{d}, setPAutocorrelation/{a}, setPMean/{m}, setPStdDeviation/{s}, setP_U_Characteristics, setP_f_Characteristics, setQAutocorrelation/{a}, setQMean/{m}, setQStdDeviation/{s}, setQ_U_Characteristics, setQ_f_Characteristics, startSchedule, stopSchedule, startLoad, stopLoad, };


	public static class WS
	{ 
		private static string Client = "GenericLoadWS";

		public static string DownloadXML(string function, string hostname, string port)
		{
			string url = "http://" + hostname + ":" + port + "/" + Client + "/" + function;
			string xml;
			using (var webClient = new WebClient())
			{try{
					xml = webClient.DownloadString(url);
				}
				catch{
					return "NAN";
				}
			}

			XDocument doc = XDocument.Parse(xml);

			var value = doc.Root.Element("value").Value;
			var timestamp = doc.Root.Element("timestampMicros").Value;
			return value.Substring(0,4);
		}
		public static string DownloadXML(string function)
		{


			return DownloadXML (function, "localhost", "8080");
		}
	}
}

