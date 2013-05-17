using System.Linq;
using Courses.RavenDB.London.May2013.Controllers;
using Courses.RavenDB.London.May2013.Models;
using Raven.Client;

namespace Courses.RavenDB.London.May2013.Indexes
{
	public class LazyController : RavenController
	{
		 public object HomePage()
		 {
			 
			 var dogs = Session.Query<Dog>().Lazily();
			 var users = Session.Query<User>().Lazily();
			 var cats = Session.Query<Cat>().Lazily();

			 return Json(new
				 {
					 Dogs = dogs.Value,
					 Cats = cats.Value,
					 Users = users.Value
				 });
		 }
	}
}