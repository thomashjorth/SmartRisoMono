using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
namespace DataAggregator.Models
{
	[DataContract]
	public class DER{
		[DataMember]

		internal string hostname;

		[DataMember]
		internal string port;

		[DataMember]
		internal string type;

		[DataMember]
		internal string role;

		public static List<DER> DeserialiseDerJsonList(string json){
			DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<DER>));

			MemoryStream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			writer.Write(json);
			writer.Flush();
			stream.Position = 0;

			List<DER> list = (List<DER>)ser.ReadObject(stream);
			return list;
		}
	}


}

