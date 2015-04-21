using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;

namespace DataAggregator.Models
{

	public enum Type {
		NAN, GenericLoadWS
	};
	public enum Role {
		PowerConsumerWS, NAN
	};

	public class App{
		public string Name;
		public bool Status;
	

		public App(string name, bool status){
			Name = name;
			Status = status;
		}
		
	}



	public class DER{

	
		public DER(string hostname, string port){
			Hostname = hostname;
			Port = port;
			Apps = appNames();
			WsInterface = "NAN";
			determineWsInterface ();
		}
		public HashSet<string> Types = new HashSet<string>();
		public HashSet<string> Roles = new HashSet<string>();

		public string Hostname;

		public string Port;

		public List<App> Apps;

		public string WsInterface;

		private List<App> appNames(){
			List<App> apps = new List<App> ();
			string url = "http://" + Hostname+":8084/AppViewWSServer/getNumberOfApps";
			string xml;
			using (var webClient = new WebClient())
			{try{
					xml = webClient.DownloadString(url);
				}
				catch{
					return apps;
				}
			}
			try{
				XDocument doc = XDocument.Parse(xml);	
				int numberOfApps =int.Parse(doc.Element("integer").Value);
				string urlApp;

				for (int i = 0; i < numberOfApps; i++) {
					

					urlApp = "http://" + Hostname+":8084/AppViewWSServer/getApp/"+i;

					using (var webClient = new WebClient())
					{try{
							xml = webClient.DownloadString(urlApp);			
							XDocument doc2 = XDocument.Parse(xml);
							bool status = false;
							if(doc2.Root.Attribute("status").Value.Equals("Running"))
							{
								status = true;
							}
							apps.Add (
								new App(
									
									doc2.Root.Attribute("name").Value
									,status)
							);
						}
						catch{
							apps.Add(new App("NAN1 :"+urlApp,false));
						}
					}

				}
			}catch{
				apps.Add (new App("NAN2",false));
			}
			return apps;
		}

		private void determineWsInterface(){
			foreach (App app in Apps) {
				if (app.Name.Contains ("Dumpload")) {
					Types.Add (Type.GenericLoadWS.ToString());
					WsInterface= Type.GenericLoadWS.ToString();
				}else if(app.Name.Contains ("Mobile load WS")){
					Types.Add (Type.GenericLoadWS.ToString());
					WsInterface= Type.GenericLoadWS.ToString();
				} 
			}
		}
	}


}

