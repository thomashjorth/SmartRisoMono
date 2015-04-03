using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Net;
using System.IO;
using MonDiaVisualization.Models;

namespace MonDiaVisualization.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index ()
		{
			//Nagios nag = new Nagios();
			//nag.Status = 50;

			String result = null;
			String url = "http://syslab-00.risoe.dk/nagstatus";
			WebResponse response = null;
			StreamReader reader = null;
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

			request.Method = "GET";
			response = request.GetResponse();
			reader = new StreamReader(response.GetResponseStream());
			List<Nagios> statusDict = new List<Nagios>();
			List<String> elementBuffer = new List<String>();
			bool inside = false;
			while ((result = reader.ReadLine()) != null)
			{
				Console.WriteLine(result);
				// started section
				if (result.EndsWith("hoststatus {") && !inside)
				{
					inside = true;
				}
				// adding section
				if (inside)
				{
					elementBuffer.Add(result);
				}
				// finished section
				if (result.EndsWith("}"))
				{
					inside = false;
					Nagios nag = new Nagios();
					string hostname = null;
					string status = null;
					foreach (var s in elementBuffer)
					{
						if (s.StartsWith("\thost_name="))
						{
							hostname = TextFollowing(s, "host_name=");
						}
						if (s.StartsWith("\tplugin_output="))
						{
							status = TextFollowing(s, "plugin_output=");
							if (status.StartsWith("PING OK")) {
								status = "OK";
							} 
							else if (status.StartsWith("CRITICAL")) {
								status = "CRITICAL";
							}
						}
					}

					nag.Name = hostname;
					nag.Status = status;
					statusDict.Add(nag);
				}
			}

			Dictionary<String, int> testDict = new Dictionary<string, int>();
			testDict.Add("a", 1);
			testDict.Add("b", 2);
			ViewBag.Test = testDict;

			List<String> testList = new List<string>();
			testList.Add("c");
			testList.Add("d");
			ViewBag.List = testList;

			ViewBag.Response = statusDict;
			ViewBag.Status = 101;
			return View();
		}

		public static string TextFollowing(string searchTxt, string value)
		{
			if (!String.IsNullOrEmpty(searchTxt) && !String.IsNullOrEmpty(value))
			{
				int index = searchTxt.IndexOf(value);
				if (-1 < index)
				{
					int start = index + value.Length;
					if (start <= searchTxt.Length)
					{
						return searchTxt.Substring(start);
					}
				}
			}
			return null;
		}
	}
}

