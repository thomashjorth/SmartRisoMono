using System;
using System.Threading;
using System.IO;
using System.Text;
using System.Net;
using DataAggregator.Utils;

namespace Data
{
	public class PowerReadingCollector
	{
		private bool SaveReadings(StringBuilder sb){

			string homePath = (Environment.OSVersion.Platform == PlatformID.Unix || 
				Environment.OSVersion.Platform == PlatformID.MacOSX)
				? Environment.GetEnvironmentVariable("HOME")
				: Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
			
			string powerSaveDir = homePath + "/DataAggregatorData/WashingMachine/Power/";
			try{
				Directory.CreateDirectory(powerSaveDir);
				File.AppendAllText (powerSaveDir + DateTime.Now.ToString ("yyyyMMdd") + ".csv", sb.ToString ());
				Console.WriteLine("Appending: " + powerSaveDir + DateTime.Now.ToString ("yyyyMMdd") + ".csv");
				return true;
			}catch{
				if(!File.Exists(powerSaveDir + DateTime.Now.ToString ("yyyyMMdd") + ".csv"))
				{
					Console.WriteLine("New: " + powerSaveDir + DateTime.Now.ToString ("yyyyMMdd") + ".csv");
					File.Create (powerSaveDir + DateTime.Now.ToString ("yyyyMMdd") + ".csv");
					File.AppendAllText (powerSaveDir + DateTime.Now.ToString ("yyyyMMdd") + ".csv", "TIMESTAMP;POWER");

				}
				Console.WriteLine ("sleeps and tries again");
				Thread.Sleep (50);
				SaveReadings (sb);
				return false;
			}
		}
		public void DoWork()
		{
			StringBuilder sb = new StringBuilder ();  
			//sb.AppendLine ("timestamp;power"); 
			int count = 0;
			while (!_shouldStop)
			{
				
				double[] readingDl = Utils.downloadReading ();

				if (readingDl!=null && readingDl.Length != 0) {
					sb.AppendLine (readingDl [0] / 1000 + ";" + readingDl [1]); 
				}		
					Thread.Sleep(1000);
					count++;
					if (count==5) {
						SaveReadings (sb);
						sb = new StringBuilder ();
						count = 0;
					}


				}
			Console.WriteLine("Collection Stopped");
		}
		public void RequestStop()
		{
			_shouldStop = true;
		}
		// Volatile is used as hint to the compiler that this data 
		// member will be accessed by multiple threads. 
		private volatile bool _shouldStop;
	}
}

