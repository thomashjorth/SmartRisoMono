using System;

namespace DataModel
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

