using Courses.RavenDB.London.May2013.Models;

namespace Courses.RavenDB.London.May2013.Controllers
{
	public class BulkController : RavenController
	{
		 public object Users()
		 {
			 for (int i = 0; i < 1000; i++)
			 {
				using (var s = DocumentStore.OpenSession())
				{
					for (int j = 0; j < 1000; j++)
					{
						s.Store(new User {Name = "User-" + i + "-" + j});
					}
					s.SaveChanges();
				}
			 }
			 return Json("ok");
		 }
	}
}