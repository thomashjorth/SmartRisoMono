using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using DataAggregator.Models;
using System.IO;
using System.Runtime.Serialization.Json;

namespace DataAggregator.Controllers
{
	public class CirclePackingController : ApiController
    {

		public string Get(string id)
		{
			Circle c = new Circle ();
			c.name = "test";
			c.size = 690;
			c.children = new List<Circle> {};
			Circle c1 = new Circle ();
			c1.name = "hej";
			c1.size = 150;
			c1.children = new List<Circle>{ };
			c.children.Add (c1);

			return Utils.JSONUtil.ToJSON(c);
		}
    }
}
