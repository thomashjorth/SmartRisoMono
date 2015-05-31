using System;
using System.Collections.Generic;
using System.IO;
using DataModel.Syslab;

namespace DataModel
{

	public class LabeledMeasurement{
		public LabeledMeasurement(string l, CompositeMeasurement i){
			label = l;
			measurement = i;
		}
		public string label;
		public CompositeMeasurement measurement;
	}
}
