using Courses.RavenDB.London.May2013.Indexes;
using Courses.RavenDB.London.May2013.Models;
using System.Linq;

namespace Courses.RavenDB.London.May2013.Controllers
{
	public class SearchController : RavenController
	{
		public object Name(string name)
		{
			var r = Session.Query<IHaveName>("Names/Search")
				.FirstOrDefault(x => x.Name == name);
			if (r == null)
			{
				return Json("Not found");
			}

			var haveOwner = r as IHaveOwner;
			if (haveOwner != null)
			{
				var owner = Session.Load<User>(haveOwner.OwnerId);

				return Json(new
					{
						r.Name,
						Owner = owner.Name
					});
			}

			var user = (User) r;
			var animals = Session.Query<IHaveOwner, Animals_ByOwner>()
			       .Where(x => x.OwnerId == user.Id)
				   .OfType<IHaveName>()
			       .ToList();

			return Json(new
				{
					Owner = user.Name,
					Pets = animals.Select(x=>x.Name)
				});
		} 
	}
}