using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using MonDiaExample.Models;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Web.Http;


namespace MonDiaExample.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index ()
		{
			var mvcName = typeof(Controller).Assembly.GetName ();
			var isMono = Type.GetType ("Mono.Runtime") != null;

			ViewData ["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
			ViewData ["Runtime"] = isMono ? "Mono" : ".NET";

			return View ();
		}
		
	}
}

