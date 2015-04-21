using System;

namespace DataModel
{
	public class TSTP
	{
		public TSTP(long sec){
			seconds = sec;
		}
		private long seconds;

		public long Seconds {
			get {
				return seconds;
			}
		}
	}

}

