using System;
using System.IO;
using Accord.Math;
using Accord.MachineLearning;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using DataAggregator.Models;

namespace Data
{
	public class WashingMachineClustering
	{
		public void DoWork()
		{
			
			while (!_shouldStop)
			{
				ApplianceClustering (3);
				Thread.Sleep (10000);
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


		public double[] readCSV(string CSVFilePathName, int Coloum){
			string[] Lines = File.ReadAllLines(CSVFilePathName);
			string[] Fields;
			int numberOfLines = Lines.GetLength (0);
			double[]
			dt = new double[numberOfLines-1];

			for (int i = 1; i < numberOfLines; i++)
			{
				Fields = Lines[i].Split(new char[] { ';' });
				dt [i-1] = Double.Parse(Fields[Coloum]);
			}
			return dt;
		}

		public  List<double> loadCSV(string homePath, int csvIndex){
			string[] dirs;
			List<double[]> powerM = new List<double[]>{ };
			List<double> powerConcat = null;
			int length = 0;
			try 
			{

				dirs = Directory.GetFiles(homePath+"/DataAggregatorData/WashingMachine/Power/","*.csv");
				Console.WriteLine("Files: " + dirs.Length);

				// Read Power

				for(int file =0; file<dirs.Length; file++){
					double[] currentRead;
					try{
						currentRead = readCSV (dirs[file], csvIndex);
					}catch(Exception e){
						Console.WriteLine("empty file: " + dirs[file] + " " + e.ToString());
						currentRead = null;
					}
					if (currentRead !=null) {
						powerM.Add (currentRead);
						length += powerM [file].Length;
					}
					Console.WriteLine(dirs[file]);
					Console.WriteLine(length);
				}

				// Concatenate Power
				powerConcat = new List<double>{};
				foreach(double[] file in powerM){
					foreach(double mes in file){
						powerConcat.Add(mes);
					}
				}
				Console.WriteLine(powerConcat.Count);


			} catch(Exception e){
				Console.WriteLine (e.ToString());
				powerConcat = new List<double>{};
			}
			return powerConcat;
		}

		public void ApplianceClustering (int numberOfPrograms )
		{
			DateTime epoch = new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			// Get Home Path

			string homePath = (Environment.OSVersion.Platform == PlatformID.Unix ||
			                  Environment.OSVersion.Platform == PlatformID.MacOSX)
				? Environment.GetEnvironmentVariable ("HOME")
				: Environment.ExpandEnvironmentVariables ("%HOMEDRIVE%%HOMEPATH%");
			
			List<double> powerConcat = loadCSV (homePath, 1);
			List<double> timeConcat = loadCSV (homePath, 0);

			// From double[] to double[][] to work with Accord
			double[][] powerInput = new double[powerConcat.Count][];
			for (int i = 0; i < powerConcat.Count; i++) {
				powerInput [i] = new double[]{ powerConcat [i] };
			}


			// Clustering with k=2
			KMeans	kmean = new KMeans (2, Distance.Euclidean);
			kmean.Compute (powerInput);

			int duration = 0;
			double powerForDuration = 0.0;
			int currentClass = -1;
			List<double[]> preparedData = new List<double[]>{ };
			string res0 = "";
			res0 += "Class;Duration;PowerkWh\n";
			for (int p = 0; p < powerInput.Length; p++) {

				if (currentClass != kmean.Clusters.Nearest (powerInput [p])) {
					currentClass = kmean.Clusters.Nearest (powerInput [p]);
					res0 += currentClass + "; " + duration + "; " + powerForDuration / 1000 / 60 / 60 / 60 + " " + epoch.AddSeconds (timeConcat [p] / 1000) + " " + TimeSpan.FromSeconds (duration) + "\n";
					if (duration != 0) {
						preparedData.Add (new double[]{ powerForDuration / duration, powerForDuration });
					}
					duration = 0;
					powerForDuration = 0.0;
				}
				duration++;
				powerForDuration = powerForDuration + powerConcat [p];
			}
			res0 += currentClass + "; " + duration + "; " + powerForDuration / 1000 / 60 / 60 / 60 + " " + epoch.AddSeconds (timeConcat.Last () / 1000) + " " + TimeSpan.FromSeconds (duration) + "\n";
			preparedData.Add (new double[]{ powerForDuration/duration, powerForDuration });
			Console.WriteLine (res0);

			// CLustering with k=4
			KMeans	kmean2 = new KMeans (numberOfPrograms
				, Distance.Euclidean);
			kmean2.Compute (preparedData.ToArray ());
			string res = "";
			res += "cluster;duration;power\n";
			foreach (double[] data in preparedData) {
				res +=
				kmean2.Clusters.Nearest (data) + "; " + data [0] + "; " + data [1] + "\n"; 
			}
			Console.WriteLine (res);


			// Centroids to json
			List<double> centroidsPower = new List<double>{};
			List<double> centroidsEnergy = new List<double>{};

			foreach (double[] cen in kmean2.Clusters.Centroids) {
				centroidsPower.Add (cen[0]);
				centroidsEnergy.Add (cen[1]);
			}

			centroidsPower.Sort ();
			centroidsEnergy.Sort ();

			// Saving Json with Power Centroids
			List<LabeledInstance> centroidWithLabel = new List<LabeledInstance>{};
			centroidWithLabel.Add(new LabeledInstance ("Standby + Other", centroidsPower [0]));
			centroidWithLabel.Add(new LabeledInstance ("40 Half", centroidsPower [1]));
			centroidWithLabel .Add(new LabeledInstance ("60 Half", centroidsPower[2]));
			if (numberOfPrograms == 4) {
				centroidWithLabel.Add (new LabeledInstance ("60 Full", centroidsPower [3]));
			}
			string cenJSON = Newtonsoft.Json.JsonConvert.SerializeObject (centroidWithLabel);
			File.Delete (homePath + "/DataAggregatorData/WashingMachine/centroidsPower.json");
			File.AppendAllText (homePath+"/DataAggregatorData/WashingMachine/centroidsPower.json", cenJSON);

			// Saving Json with Energy Centroids
			List<LabeledInstance> centroidWithLabelEnergy = new List<LabeledInstance>{};
			centroidWithLabelEnergy.Add(new LabeledInstance ("Standby + Other", centroidsEnergy [0]/60/60/1000000));
			centroidWithLabelEnergy.Add(new LabeledInstance ("40 Half", centroidsEnergy [1]/60/60/1000000));
			centroidWithLabelEnergy .Add(new LabeledInstance ("60 Half", centroidsEnergy[2]/60/60/1000000));
			if (numberOfPrograms == 4) {
				centroidWithLabelEnergy.Add (new LabeledInstance ("60 Full", centroidsEnergy [3] / 60 / 60 / 1000000));
			}
			string cenJSONEnergy = Newtonsoft.Json.JsonConvert.SerializeObject (centroidWithLabelEnergy);
			File.Delete (homePath + "/DataAggregatorData/WashingMachine/centroidsEnergy.json");
			File.AppendAllText (homePath+"/DataAggregatorData/WashingMachine/centroidsEnergy.json", cenJSONEnergy);

			// Saving count of programs detected
			List<LabeledInstance> programsCountJSON = new List<LabeledInstance>{};
			programsCountJSON.Add(new LabeledInstance("40 Half",0));
			programsCountJSON.Add(new LabeledInstance("60 Half",0));
			if (numberOfPrograms == 4) {
				programsCountJSON.Add (new LabeledInstance ("60 Full", 0));
			}
			foreach (double[] data in preparedData) {
				if (kmean2.Clusters.Centroids[kmean2.Clusters.Nearest (data)][0] == centroidsPower [1]) {
					programsCountJSON [0].value++;
				}else if(kmean2.Clusters.Centroids[kmean2.Clusters.Nearest (data)][0] == centroidsPower [2]){
					programsCountJSON [1].value++;
				}if (numberOfPrograms == 4) {
				 if(kmean2.Clusters.Centroids[kmean2.Clusters.Nearest (data)][0] == centroidsPower [3]){
					programsCountJSON [2].value++;
					}}
			}
			string programsCountJs = Newtonsoft.Json.JsonConvert.SerializeObject (programsCountJSON);
			File.Delete (homePath + "/DataAggregatorData/WashingMachine/programsCount.json");
			File.AppendAllText (homePath+"/DataAggregatorData/WashingMachine/programsCount.json", programsCountJs);
		}

	}
}