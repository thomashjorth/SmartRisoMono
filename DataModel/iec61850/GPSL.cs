using System;

namespace DataModel
{
	public class GPSL
	{

		private double latitude;
		private double longitude;
		private double altitude;
		private TSTP time;

		public GPSL ()
		{
		}

		public double Latitude {
			get {
				return latitude;
			}
		}

		public double Longitude {
			get {
				return longitude;
			}
		}

		public double Altitude {
			get {
				return altitude;
			}
		}

	}
}

