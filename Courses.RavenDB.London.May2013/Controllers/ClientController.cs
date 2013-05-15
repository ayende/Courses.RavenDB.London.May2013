using System.Web.Mvc;
using Raven.Client.Document;

namespace Courses.RavenDB.London.May2013.Controllers
{
	public class ClientController : Controller
	{
		public object ShowDog()
		{
			using (var store = new DocumentStore
				{
					Url = "http://localhost:8080",
					DefaultDatabase = "Courses"
				}.Initialize())
			{
				return Content(store.DatabaseCommands.Get("dogs/arava").DataAsJson.ToString());
			}	
		}
		 
	}
}