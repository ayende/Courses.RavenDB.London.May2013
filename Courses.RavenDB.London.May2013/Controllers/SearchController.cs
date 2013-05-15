using Courses.RavenDB.London.May2013.Models;
using System.Linq;

namespace Courses.RavenDB.London.May2013.Controllers
{
	public class SearchController : RavenController
	{
		public object Name(string name)
		{
			var r = Session.Query<IHaveName>()
			       .Where(x => x.Name == name)
			       .ToList();

			return Json(r);
		} 
	}
}