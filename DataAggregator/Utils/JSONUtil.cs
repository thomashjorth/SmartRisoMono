using System.IO;
using System.Runtime.Serialization.Json;
namespace DataAggregator.Utils
{
	public class JSONUtil
	{
		public static string ToJSON(object obj)
		{
			//JavaScriptSerializer serializer = new JavaScriptSerializer();
			//return serializer.Serialize(obj);
			MemoryStream stream1 = new MemoryStream();
			DataContractJsonSerializer ser = new DataContractJsonSerializer(obj.GetType());

			ser.WriteObject(stream1, obj);
			stream1.Position = 0;
			StreamReader sr = new StreamReader(stream1);
			return sr.ReadToEnd();
		}
	}
}

