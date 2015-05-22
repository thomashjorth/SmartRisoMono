using System;

namespace DataModel.Syslab
{
	public class CompositeBoolean : CompositeData
	{
		public bool value;

		public CompositeBoolean () 
		{

		}
		public CompositeBoolean (bool val) 
		{
			value = val;
		}

	}
}

