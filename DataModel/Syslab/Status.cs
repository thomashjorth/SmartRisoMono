using System;

namespace DataModel.Syslab
{
	public class Status : CompositeData
	{
		public int status;

		public Status () 
		{

		}
		public Status (int val) 
		{
			status = val;
		}

	}
}

