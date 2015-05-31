using System;
using DataModel;
using DataModel.Syslab;
using System.Collections.Generic;

namespace DataAggregator
{
	public static class UnitMerging
	{

		public static CompositeMeasurementAggregated CombineData(List<string> hosts, string device, string action){
			List<LabeledMeasurement> measurements = GetData (hosts, device, action);

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

		public static List<LabeledMeasurement> GetData(List<string> hosts, string device, string action){

			List<LabeledMeasurement> data = new List<LabeledMeasurement> ();
			foreach(var host in hosts){
				var temp = host.Split (':');
				data.Add(new LabeledMeasurement(host,Utils.WS.convertXMLToCompositeMeasurement(device, action, temp [0], temp [1])));
			}
			return data;
		}
	}
}

