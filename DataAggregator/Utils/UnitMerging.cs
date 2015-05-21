using System;
using DataModel;
using System.Collections.Generic;

namespace DataAggregator
{
	public static class UnitMerging
	{

		public static CompositeMeasurementAggregated CombineData(List<string> hosts, string device, string action){
			List<CompositeMeasurement> measurements = GetData (hosts, device, action);

			CompositeMeasurementAggregated aggregated = new CompositeMeasurementAggregated ();

			foreach (var measurement in measurements) {
				aggregated.timestampMicros += measurement.timestampMicros;
				aggregated.value += measurement.value;
			}

			aggregated.timestampMicros = aggregated.timestampMicros / measurements.Count;
			aggregated.avg = aggregated.value / measurements.Count;
			aggregated.count = measurements.Count;

			return aggregated;
		}

		public static List<CompositeMeasurement> GetData(List<string> hosts, string device, string action){

			List<CompositeMeasurement> data = new List<CompositeMeasurement> ();
			foreach(var host in hosts){
				var temp = host.Split (':');
				data.Add(Utils.WS.convertXMLToCompositeMeasurement(device, action, temp [0], temp [1]));
			}
			return data;
		}
	}
}

