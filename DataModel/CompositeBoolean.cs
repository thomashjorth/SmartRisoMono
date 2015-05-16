using System;

namespace DataModel
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

