using System;
using DataModel.ConfigurationModel.Classes;
using System.Collections.Generic;

namespace DataModel.ConfigurationModel
{
	public abstract class AbstractPageFactory
	{
		public abstract PageConfig CreatePage(List<VisualizationConfig> visualizations);
		public abstract PagesConfig CreatePages(List<VisualizationConfig> visualizations);
	}

	public class EquallySized3x3PageFactory : AbstractPageFactory{
		public override PageConfig CreatePage(List<VisualizationConfig> visualizations)
		{
			if (visualizations.Count < 10) {
				return new PageConfig (visualizations);
			} else {
				throw new Exception ("Number of visualizations on page not in accepted interval [1-9]");
			}

		}

		public override PagesConfig CreatePages(List<VisualizationConfig> visualizations)
		{
			EquallySized3x3PageFactory factory = new EquallySized3x3PageFactory ();
			PagesConfig pages = new PagesConfig(new List<PageConfig>() { });
			List<VisualizationConfig> temp = new List<VisualizationConfig>(){};
			int count = 0;
			foreach (VisualizationConfig v in visualizations) {
				temp.Add (v);
				count++;
				if (count == 9) {
					pages.Pages.Add(
						factory.CreatePage (temp)
					);
					temp = new List<VisualizationConfig> (){ };
					count = 0;
				}

			}
			// The remains
			pages.Pages.Add (
				factory.CreatePage (temp));
			return pages;
		}
	

	}


}

