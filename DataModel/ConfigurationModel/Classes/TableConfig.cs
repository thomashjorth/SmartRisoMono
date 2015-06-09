namespace DataModel.ConfigurationModel.Classes
{
	public class TableConfig : VisualizationConfig
	{
		public string TitleHeading;
		public string VisualizationType;
		public TableConfig ( string host, int port, string device, string resource, int updateIterval, string titleHeading) 
			: base( host, port, device, resource, updateIterval)
		{
			VisualizationType = "table";
			TitleHeading = titleHeading;
		}
	}
}

