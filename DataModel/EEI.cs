using System;

namespace DataModel
{
	public class EEI
	{
		int Capacity;
		double[] ProgramEnergy;
		public EEI (int c, double[] programEnergy)
		{
			Capacity = c;
			ProgramEnergy = programEnergy;
		}

		public double SAEC(){
			return 47.0*Capacity+51.7;
		}

		public double E(){
			return (
				3 * ProgramEnergy [0] + 
				2 * ProgramEnergy [1] + 
				2 * ProgramEnergy [2]) 
				/ 7;
		}
		public double AEC(){
			return E()*220;
		}

		public double EeiScore(){
			return  AEC () / SAEC () * 100;
		}

		public string Rating(){
			double score = EeiScore ();
			if (score < 46) {
				return "A+++";
			}else if (score < 52) {
				return "A++";
			}else if (score < 59) {
				return "A+";
			}else if (score < 68) {
				return "A";
			}else if (score < 77) {
				return "B";
			}else if (score < 87) {
				return "C";
			}else {
				return "D";
			}
		}



	}
}

