using System;
using System.Web.Script.Serialization;

namespace DataAggregator.Utils
{
	public class JSONUtil
	{
		public static string ToJSON(object obj)
		{
			JavaScriptSerializer serializer = new JavaScriptSerializer();
			return serializer.Serialize(obj);
		}

		public static string ToJSON(object obj, int recursionDepth)
		{
			JavaScriptSerializer serializer = new JavaScriptSerializer();
			serializer.RecursionLimit = recursionDepth;
			return serializer.Serialize(obj);
		}
	}
}

