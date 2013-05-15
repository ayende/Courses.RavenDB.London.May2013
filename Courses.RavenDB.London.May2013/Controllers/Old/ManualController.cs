using System.Net;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace Courses.RavenDB.London.May2013.Controllers
{
	public class ManualController : Controller
	{
		protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
		{
			return base.Json(data, contentType, contentEncoding, JsonRequestBehavior.AllowGet);
		}

		public object Try()
		{
			var webClient = new WebClient
				{
					BaseAddress = "http://localhost.fiddler:8080/databases/courses/"
				};

			var dogStr = webClient.DownloadString("docs/dogs/arava");
			var dogObj = JObject.Parse(dogStr);

			var ownerStr = webClient.DownloadString("docs/" + dogObj["Owners"][0].Value<string>());

			var ownerObj = JObject.Parse(ownerStr);

			return Json(new
				{
					Dog = dogObj.Value<string>("Name"),
					Owner = ownerObj.Value<string>("Name")
				});

		}
		 
	}
}