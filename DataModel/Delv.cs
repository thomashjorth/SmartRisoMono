using System;

namespace DataModel
{
	public class Delv
	{

		public CompositeMeasurement phaseAB;
		public CompositeMeasurement phaseAverage;
		public CompositeMeasurement phaseBC;
		public CompositeMeasurement phaseCA;

		public Delv (CompositeMeasurement ab, CompositeMeasurement avg, CompositeMeasurement bc, CompositeMeasurement ca)
		{
			phaseAB = ab;
			phaseAverage = avg;
			phaseBC = bc;
			phaseCA = ca;
		}
	}
}

