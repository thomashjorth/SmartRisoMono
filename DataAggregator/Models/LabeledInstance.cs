using System;
using System.Collections.Generic;
using DataAggregator.Models;
using DataAggregator.Utils;
using System.IO;
using Newtonsoft.Json;
using DataModel;

namespace DataAggregator.Models
{

	public class LabeledInstance{
		public LabeledInstance(string l, double i){
			label = l;
			value = i;
		}
		public string label;
		public double value;
	}
}
