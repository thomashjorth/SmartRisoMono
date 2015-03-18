using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Visualization.Controllers
{
	public class VisualizationController : Controller
	{
		// GET: Visualization
		public ActionResult Gauge()
		{
			return View();
		}
		// GET: Visualization
		public ActionResult Graph()
		{
			return View();
		}
		// GET: Visualization
		public ActionResult d3Graph()
		{
			return View();
		}
		// GET: Visualization
		public ActionResult d3Gauge()
		{
			return View();
		}
	}
}
