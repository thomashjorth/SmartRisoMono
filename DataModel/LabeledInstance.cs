using System;
using System.Collections.Generic;
using System.IO;

namespace DataModel
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
