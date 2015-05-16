using System;
using System.IO;
using System.Net;
using DataAggregator.Utils;

namespace Data
{
	public static class Utils
	{
		public static double[] downloadReading(){
			string url = "http://localhost:8080/GenericLoadWS/getActivePower";
			string xml;

			var activePower = WS.convertXMLToComposite ("GeneriLoadWS", "getActivePower", "localhost", "8080");

			return new double[] {
				activePower.timestampMicros,
				activePower.value
			}; 
		}
			public static double[][] readCSV(string CSVFilePathName ){
			string[] Lines = File.ReadAllLines(CSVFilePathName);
			string[] Fields;

			int numberOfLines = Lines.GetLength (0);
			double[][] dt = new double[numberOfLines-1][];

			Fields = Lines[0].Split(new char[] { ';' });
			int Cols = Fields.GetLength(0);
			//1st row must be column names; force lower case to ensure matching later on.
			// i=1 to skip headings
			for (int i = 1; i < numberOfLines; i++)
			{
				Fields = Lines[i].Split(new char[] { ';' });
				dt [i-1] = new double[]{ Double.Parse(Fields[1]) };
			}
			return dt;
		}

		public static int[] readCSVTimestamps(string CSVFilePathName){
			string[] Lines = File.ReadAllLines(CSVFilePathName);
			string[] Fields;
			int numberOfLines = Lines.GetLength (0);
			int[] dt = new int[numberOfLines-1];

			Fields = Lines[0].Split(new char[] { ';' });

			for (int i = 1; i < numberOfLines; i++)
			{
				
				Fields = Lines[i].Split(new char[] { ';' });
				var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
				int seconds = (int)(double.Parse (Fields [0])/1000);
				dt [i - 1] = seconds;

			}
			return dt;
		}
	}
}

