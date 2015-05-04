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
			try{
				Console.WriteLine("Appending: " + @"../../Data/WashingMachine/Power/" + DateTime.Now.ToString ("yyyyMMdd") + ".csv");
				File.AppendAllText (@"../../Data/WashingMachine/Power/" + DateTime.Now.ToString ("yyyyMMdd") + ".csv", sb.ToString ());
				return true;
			}catch{
				if(
					!File.Exists(
						@"../../Data/WashingMachine/Power/" + DateTime.Now.ToString ("yyyyMMdd") + ".csv"
					)

				){
					Console.WriteLine("New: " + @"../../Data/Washingmachine/Power/" + DateTime.Now.ToString ("yyyyMMdd") + ".csv");

					File.Create (@"../../Data/Washingmachine/Power/" + DateTime.Now.ToString ("yyyyMMdd") + ".csv"
					);
					File.AppendAllText (@"../../Data/WashingMachine/Power/" + DateTime.Now.ToString ("yyyyMMdd") + ".csv", "TIMESTAMP;POWER");

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

				sb.AppendLine (readingDl[0]/1000+";"+readingDl[1]); 
							
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

