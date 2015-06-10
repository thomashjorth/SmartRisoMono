using System;

namespace DataModel
{
	public class EEI
	{
		int Capacity,w0,w1,w2;
		double[] ProgramEnergy;

		public EEI (int c, double[] programEnergy)
		{
			Capacity = c;
			ProgramEnergy = programEnergy;
		}

		public EEI (int c, double[] programEnergy, int w0, int w1, int w2)
		{
			Capacity = c;
			ProgramEnergy = programEnergy;
			this.w0 = w0;
			this.w1 = w1;
			this.w2 = w2;

		}

		public double SAEC(){
			return 47.0*Capacity+51.7;
		}

		public double E(int w0,int w1,int w2){
			if (w0 == 0 && w1 == 0 && w2 == 0) {
				return (
				    3 * ProgramEnergy [0] +
				    2 * ProgramEnergy [1] +
				    2 * ProgramEnergy [2])
				/ 7;
			} else {
				return (
					w0 * ProgramEnergy [0] +
					w1 * ProgramEnergy [1] +
					w2 * ProgramEnergy [2])
					/ (w0+w1+w2);
			}
		}
		public double AEC(){
			return E(w0,w1,w2)*220;
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

