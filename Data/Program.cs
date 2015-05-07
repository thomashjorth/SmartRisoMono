using System;
using System.Threading;

namespace Data
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			// Get Home Path
			string homePath = (Environment.OSVersion.Platform == PlatformID.Unix || 
				Environment.OSVersion.Platform == PlatformID.MacOSX)
				? Environment.GetEnvironmentVariable("HOME")
				: Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

			PowerReadingCollector powerCollector = new PowerReadingCollector();
			Thread powerThread = new Thread(powerCollector.DoWork);
			powerThread.Start();


			WashingMachineClustering washing = new WashingMachineClustering();
			Thread washingThread = new Thread(washing.DoWork);
			washingThread.Start();
		}
	}
}
