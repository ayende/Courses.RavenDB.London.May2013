using System;
using System.Linq.Expressions;
using Courses.RavenDB.London.May2013.Indexes;
using Courses.RavenDB.London.May2013.Models;
using System.Linq;
using Raven.Client;

namespace Courses.RavenDB.London.May2013.Controllers
{
	public class UsersController : RavenController
	{
		public object New(string name)
		{
			var user = new User() { Name = name };
			Session.Store(user);

			//Session.Load<MyTransfomer,User>("dogs/1")

			//Session.Query<User>()
			//	.TransformWith<MyTransfomer>()


			return Json(user.Id);
		}

		public object Load(int id)
		{
			return Json(Session.Load<User>(id));
		}

		public object Query2(string name)
		{
			return Json(Session.Query<User>().FirstOrDefault(x => x.Name == name));
		}

		public object Danger()
		{
			var users = Session.Query<User>()
			   .ToList();
			return Json(users);
		}

		public object Query(string q)
		{
			var a = Session.Query<IndexQuery>("Users/Search")
						   .Search(x => x.Query, q)
						   .OfType<User>()
						   .ToList();

			//a = Session.Advanced.LuceneQuery<User>("Users/Search")
			//		  .Search("Query", q)
			//	  .ToList();

			return Json(a);
		}

		public object Phone1(string phone)
		{
			var q = Session.Query<User>()
						   .Where(x => x.Phones.Any(p => p == phone))
						   .ToString();
			return Json(q);
		}

		public object Phone2(string phone)
		{
			var q = Session.Advanced.LuceneQuery<User>()
						   .WhereEquals("Phones", phone)
						   .ToList();
			return Json(q);
		}
	}
}