namespace DataModel.ConfigurationModel
{
	public class TableConfig : VisualizationConfig
	{
		public string TitleHeading;
		public TableConfig ( string host, int port, string device, string resource, int updateIterval, string titleHeading) 
			: base( host, port, device, resource, updateIterval)
		{
			VisualizationType = "table";
			TitleHeading = titleHeading;
		}
	}
}

