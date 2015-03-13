using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace DataAggregator
{
	[DataContract(Namespace = "")] 
	internal class Circle
	{
		[DataMember]
		internal string name;

		[DataMember]
		internal int size;

		[DataMember]
		internal List<Circle> children; 
	}
}

