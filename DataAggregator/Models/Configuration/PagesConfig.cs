﻿using System;
using System.Collections.Generic;

namespace DataAggregator.Models.Configuration
{
	public class PagesConfig
	{
		public List<PageConfig> Pages = new List<PageConfig>(){};
		public PagesConfig (List<PageConfig> pages)
		{
			Pages = pages;
		}
	}
}

