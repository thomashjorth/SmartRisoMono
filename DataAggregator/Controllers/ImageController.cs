using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using DataAggregator.Models;
using System.Net;
using System.Web.Hosting;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http.Headers;
using System.Web.Http;

namespace DataAggregator.Controllers
{
    public class ImageController : ApiController
    {
		public HttpResponseMessage Get(string id)
		{
			HttpResponseMessage result;
			String filePath;
			FileStream fileStream;
			Image image;
			MemoryStream memoryStream;
			try{
				 result = new HttpResponseMessage(HttpStatusCode.OK);
				filePath = HostingEnvironment.MapPath("~/Images/"+id+".jpg");
				 fileStream = new FileStream(filePath, FileMode.Open);
				 image = Image.FromStream(fileStream);
				 memoryStream = new MemoryStream();
				image.Save(memoryStream, ImageFormat.Jpeg);
				result.Content = new ByteArrayContent(memoryStream.ToArray());
				result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
				return result;
			}catch{
				result = new HttpResponseMessage (HttpStatusCode.OK);
				filePath = HostingEnvironment.MapPath ("~/Images/plug.jpg");
				fileStream = new FileStream (filePath, FileMode.Open);
				image = Image.FromStream (fileStream);
				memoryStream = new MemoryStream ();
				image.Save (memoryStream, ImageFormat.Jpeg);
				result.Content = new ByteArrayContent (memoryStream.ToArray ());
				result.Content.Headers.ContentType = new MediaTypeHeaderValue ("image/jpeg");
				return result;
			}
		}
    }
}
