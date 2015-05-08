using System;
using DataAggregator.Models;

namespace DataAggregator
{
	public class TableConfig : VisualizationConfig
	{
		public VisualizationConfig DER;
		public TableConfig ( string host, int port, string aggregation, string resource, string titleHeading, int updateIterval) 
			: base( host, port, aggregation, resource,  titleHeading,  updateIterval)
		{
			VisualizationType = "table";
		}
	}
}

