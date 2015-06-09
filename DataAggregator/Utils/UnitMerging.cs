using System;
using DataModel;
using DataModel.Syslab;
using System.Collections.Generic;

namespace DataAggregator
{
	public static class UnitMerging
	{

		public static CompositeMeasurementAggregated CombineData(List<string> hosts, string device, string action){
			List<string> actions = new List<string> ();
			List<string> devices = new List<string> ();
			foreach (var host in hosts) {
				actions.Add (action);
				devices.Add (device);
			}
			List<LabeledMeasurement> measurements = GetData (hosts, devices, actions);

			CompositeMeasurementAggregated aggregated = new CompositeMeasurementAggregated ();

			foreach (var measurement in measurements) {
				aggregated.timestampMicros += measurement.measurement.timestampMicros;
				aggregated.value += measurement.measurement.value;
			}

			aggregated.timestampMicros = aggregated.timestampMicros / measurements.Count;
			aggregated.avg = aggregated.value / measurements.Count;
			aggregated.count = measurements.Count;

			return aggregated;
		}

		public static List<LabeledMeasurement> GetData(List<string> hosts, List<string> devices, List<string> actions){
			List<LabeledMeasurement> data = new List<LabeledMeasurement> ();
			if (actions.Count == 1) {
				foreach (var host in hosts) {
					var temp = host.Split (':');
					data.Add (new LabeledMeasurement (host, Utils.WS.convertXMLToCompositeMeasurement (devices[0], actions [0], temp [0], temp [1])));
				}
			} else {
				int i = 0;
				foreach (var host in hosts) {
					var temp = host.Split (':');
					data.Add (new LabeledMeasurement (host, Utils.WS.convertXMLToCompositeMeasurement (devices[i], actions [i], temp [0], temp [1])));
					i++;
				}
			}
			return data;
		}
	}
}

