using System;
using System.IO;
using Accord.Math;
using Accord.MachineLearning;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using DataAggregator;
using DataModel;
using DataModel.Syslab;
namespace Data
{
	public class WashingMachineClustering
	{
		string homePath = (Environment.OSVersion.Platform == PlatformID.Unix ||
			Environment.OSVersion.Platform == PlatformID.MacOSX)
			? Environment.GetEnvironmentVariable ("HOME")
			: Environment.ExpandEnvironmentVariables ("%HOMEDRIVE%%HOMEPATH%");
		public void DoWork()
		{
			
			while (!_shouldStop)
			{
				Console.WriteLine ("New Classification in 60 s");
				Thread.Sleep (60000);
				int numberOfPrograms = 4;

				
				ApplianceClustering (numberOfPrograms);

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


			List<double> onOffCen = new List<double>{ };
			foreach (double[] c in kmean.Clusters.Centroids) {
				onOffCen.Add (c [0]);
			}
			onOffCen.Sort ();
			File.Delete (homePath + "/DataAggregatorData/WashingMachine/centroidsOnOff.json");	
			File.AppendAllText (homePath+"/DataAggregatorData/WashingMachine/centroidsOnOff.json", "On;"+onOffCen[0]+"\nOff;"+onOffCen[1]);
			

			int duration = 0;
			double powerForDuration = 0.0;
			int currentClass = -1;
			List<double[]> preparedData = new List<double[]>{ };

			List<DoubleLabel> discoveredCycles = new List<DoubleLabel> (){ };

			string res0 = "";
			res0 += "Class;Duration;PowerkWh\n";
			for (int p = 0; p < powerInput.Length; p++) {

				if (currentClass != kmean.Clusters.Nearest (powerInput [p])) {
					currentClass = kmean.Clusters.Nearest (powerInput [p]);
					res0 += currentClass + "; " + duration + "; " + powerForDuration / 1000 / 60 / 60 / 60 + " " + epoch.AddSeconds (timeConcat [p] / 1000) + " " + TimeSpan.FromSeconds (duration) + "\n";
					if (duration > 2) {
						preparedData.Add (new double[]{ powerForDuration / duration, powerForDuration });
						discoveredCycles.Add(new DoubleLabel(epoch.AddSeconds (timeConcat [p] / 1000).ToString(),"Standby"));
					}
					duration = 0;
					powerForDuration = 0.0;
				}
				duration++;
				powerForDuration = powerForDuration + powerConcat [p];
			}

			res0 += currentClass + "; " + duration + "; " + powerForDuration / 1000 / 60 / 60 / 60 + " " + epoch.AddSeconds (timeConcat.Last () / 1000) + " " + TimeSpan.FromSeconds (duration) + "\n";


			Console.WriteLine (res0);
			Console.WriteLine ("---------"+preparedData.Count);
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

			EEI eeiEU = new EEI (7,new double[]{
				centroidsEnergy [1]/60/60/1000000,
				centroidsEnergy [2]/60/60/1000000,
				centroidsEnergy [3]/60/60/1000000});
			Console.WriteLine("Score: " + eeiEU.EeiScore());
			Console.WriteLine("AEC: " + eeiEU.AEC());
			Console.WriteLine("SAEC: " + eeiEU.SAEC());
			Console.WriteLine ("Rating" + eeiEU.Rating());

			EEI eeiHIGH = new EEI (7,new double[]{
				centroidsEnergy [3]/60/60/1000000,
				centroidsEnergy [3]/60/60/1000000,
				centroidsEnergy [3]/60/60/1000000});
			Console.WriteLine("Score: " + eeiHIGH.EeiScore());
			Console.WriteLine("AEC: " + eeiHIGH.AEC());
			Console.WriteLine("SAEC: " + eeiHIGH.SAEC());
			Console.WriteLine ("Rating" + eeiHIGH.Rating());

			EEI eeiMIDDLE = new EEI (7,new double[]{
				centroidsEnergy [2]/60/60/1000000,
				centroidsEnergy [2]/60/60/1000000,
				centroidsEnergy [2]/60/60/1000000});
			Console.WriteLine("Score: " + eeiMIDDLE.EeiScore());
			Console.WriteLine("AEC: " + eeiMIDDLE.AEC());
			Console.WriteLine("SAEC: " + eeiMIDDLE.SAEC());
			Console.WriteLine ("Rating" + eeiMIDDLE.Rating());

			EEI eeiLOW = new EEI (7,new double[]{
				centroidsEnergy [1]/60/60/1000000,
				centroidsEnergy [1]/60/60/1000000,
				centroidsEnergy [1]/60/60/1000000});
			Console.WriteLine("Score: " + eeiLOW.EeiScore());
			Console.WriteLine("AEC: " + eeiLOW.AEC());
			Console.WriteLine("SAEC: " + eeiLOW.SAEC());
			Console.WriteLine ("Rating" + eeiLOW.Rating());

			// Saving Json with Power Centroids
			List<LabeledMeasurement> centroidWithLabel = new List<LabeledMeasurement>{};
			centroidWithLabel.Add(new LabeledMeasurement ("Standby + Misc", new CompositeMeasurement(centroidsPower [0])));
			centroidWithLabel.Add(new LabeledMeasurement ("Low: " + eeiLOW.Rating(), new CompositeMeasurement(centroidsPower [1])));
			centroidWithLabel .Add(new LabeledMeasurement ("Middle: " + eeiMIDDLE.Rating(),new CompositeMeasurement( centroidsPower[2])));
			if (numberOfPrograms == 4) {
				centroidWithLabel.Add (new LabeledMeasurement ("High: " + eeiHIGH.Rating(), new CompositeMeasurement(centroidsPower [3])));
			}
			string cenJSON = Newtonsoft.Json.JsonConvert.SerializeObject (centroidWithLabel);
			File.Delete (homePath + "/DataAggregatorData/WashingMachine/centroidsPower.json");
			File.AppendAllText (homePath+"/DataAggregatorData/WashingMachine/centroidsPower.json", cenJSON);

			// Saving Json with Energy Centroids
			List<LabeledMeasurement> centroidWithLabelEnergy = new List<LabeledMeasurement>{};
			centroidWithLabelEnergy.Add(new LabeledMeasurement ("Standby + Other", new CompositeMeasurement(centroidsEnergy [0]/60/60/1000000)));
			centroidWithLabelEnergy.Add(new LabeledMeasurement ("Low: " + eeiLOW.Rating(), new CompositeMeasurement( centroidsEnergy [1]/60/60/1000000)));
			centroidWithLabelEnergy .Add(new LabeledMeasurement ("Middle: " + eeiMIDDLE.Rating(), new CompositeMeasurement(centroidsEnergy[2]/60/60/1000000)));
			if (numberOfPrograms == 4) {
				centroidWithLabelEnergy.Add (new LabeledMeasurement ("High: " + eeiHIGH.Rating(),new CompositeMeasurement( centroidsEnergy [3] / 60 / 60 / 1000000)));
			}
			string cenJSONEnergy = Newtonsoft.Json.JsonConvert.SerializeObject (centroidWithLabelEnergy);
			File.Delete (homePath + "/DataAggregatorData/WashingMachine/centroidsEnergy.json");
			File.AppendAllText (homePath+"/DataAggregatorData/WashingMachine/centroidsEnergy.json", cenJSONEnergy);



			// Saving count of programs detected
			List<LabeledMeasurement> programsCountJSON = new List<LabeledMeasurement>{};
			programsCountJSON.Add(new LabeledMeasurement("Low: " + eeiLOW.Rating(),new CompositeMeasurement(0)));
			programsCountJSON.Add(new LabeledMeasurement("Middle: " + eeiMIDDLE.Rating(),new CompositeMeasurement(0)));
			if (numberOfPrograms == 4) {
				programsCountJSON.Add (new LabeledMeasurement ("High: " + eeiHIGH.Rating(),new CompositeMeasurement( 0)));
			}
			Console.WriteLine (preparedData.Count + " -----------" + discoveredCycles.Count);
			for (int y=0;y<preparedData.Count;y++) {
				if (kmean2.Clusters.Centroids[kmean2.Clusters.Nearest (preparedData[y])][0] == centroidsPower [1]) {
					discoveredCycles[y].SecondField="Low: " + eeiLOW.Rating();
					programsCountJSON [0].measurement.value++;
				}else if(kmean2.Clusters.Centroids[kmean2.Clusters.Nearest (preparedData[y])][0] == centroidsPower [2]){
					programsCountJSON [1].measurement.value++;
					discoveredCycles[y].SecondField="Middle: " + eeiMIDDLE.Rating();
				}if (numberOfPrograms == 4) {
					if(kmean2.Clusters.Centroids[kmean2.Clusters.Nearest (preparedData[y])][0] == centroidsPower [3]){
						programsCountJSON [2].measurement.value++;
						discoveredCycles[y].SecondField="High: " + eeiHIGH.Rating();
					}}
			}
			discoveredCycles.Reverse ();
			string discoredCyclesJson = Newtonsoft.Json.JsonConvert.SerializeObject (discoveredCycles);
			File.Delete (homePath + "/DataAggregatorData/WashingMachine/discoveredCycles.json");
			File.AppendAllText (homePath+"/DataAggregatorData/WashingMachine/discoveredCycles.json", discoredCyclesJson);

			string programsCountJs = Newtonsoft.Json.JsonConvert.SerializeObject (programsCountJSON);
			File.Delete (homePath + "/DataAggregatorData/WashingMachine/programsCount.json");
			File.AppendAllText (homePath+"/DataAggregatorData/WashingMachine/programsCount.json", programsCountJs);
		}

	}
}