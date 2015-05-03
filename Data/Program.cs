using System;
using System.Threading;

namespace Data
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			PowerReadingCollector powerCollector = new PowerReadingCollector();
			Thread powerThread = new Thread(powerCollector.DoWork);
			powerThread.Start();
		}
	}
}
