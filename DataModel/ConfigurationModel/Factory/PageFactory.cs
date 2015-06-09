using System;
using DataModel.ConfigurationModel.Classes;
using DataModel.ConfigurationModel.Pages;
using System.Collections.Generic;

namespace DataModel.ConfigurationModel.Factory
{

	public class PageFactory{


		public  PageConfig Create3x3Page(List<VisualizationConfig> visualizations, string pageType)
		{
			if (visualizations.Count < 10) {
				return new PageConfig (visualizations,pageType);
			} else {
				throw new Exception ("Number of visualizations on page not in accepted interval [1-9]");
			}

		}
		public  MasterPageConfig CreateExperimentPage(List<ExperimentConfig> visualizations, string pageType,string hostAgg, int portAgg)
		{
			
			return new ExperimentPageConfig (visualizations,pageType,hostAgg,portAgg);
		}

		public  PagesConfig CreatePages(List<MasterPageConfig> pages)
		{
			PageFactory factory = new PageFactory ();
			PagesConfig res = new PagesConfig (new List<MasterPageConfig> (){ });
	
			foreach (MasterPageConfig pc in pages) {
				res.Pages.Add (pc);
			}
			return res;
		}

		public  PagesConfig Create3x3Pages(List<VisualizationConfig> visualizations, string pageType)
		{
			PageFactory factory = new PageFactory ();
			PagesConfig pages = new PagesConfig(new List<MasterPageConfig>() { });
			List<VisualizationConfig> temp = new List<VisualizationConfig>(){};
			int count = 0;
			foreach (VisualizationConfig v in visualizations) {
				temp.Add (v);
				count++;
			if (count == 9) {
					pages.Pages.Add(
						factory.Create3x3Page (temp,pageType)
					);
					temp = new List<VisualizationConfig> (){ };
					count = 0;
				}

			}
			// The remains
			pages.Pages.Add (
				factory.Create3x3Page (temp,pageType));
			return pages;
		}
	

	}




}

