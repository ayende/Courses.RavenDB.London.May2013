using System;
using System.Collections.Specialized;
using System.Web.Mvc;
using Courses.RavenDB.London.May2013.Indexes;
using Courses.RavenDB.London.May2013.Models;
using System.Linq;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Bundles.MoreLikeThis;

namespace Courses.RavenDB.London.May2013.Controllers
{
	public class SearchController : RavenController
	{
		public object Shared()
		{
			Session.Store(new User
				{
					Phones = Enumerable.Range(0, 50 *1000).Select(x=>x.ToString()).ToList()
				});
			return Json("boom");
		}
		
		public object ModifyWithSafety(int id, string etag)
		{
			Session.Advanced.UseOptimisticConcurrency = true;

			var user = Session.Load<User>(id);

			Session.Store(user, Etag.Parse(etag));

			user.Phones.Add("1");
			return Json("done");
		}

		public object Load(int id)
		{
			var user = Session.Load<User>(id);

			return Json(new
				{
					User = user,
					Etag = Session.Advanced.GetEtagFor(user).ToString()
				});
		}

		public object Similar(int id)
		{
			var user = Session.Load<User>(id);
			var result = Session.Advanced.MoreLikeThis<User,Users_Search>(new MoreLikeThisQuery
				{
					Fields = new[] {"Hobbies"},
					DocumentId = user.Id,
					IndexName = "Users/Search",
					MinimumDocumentFrequency = 1,
					MinimumTermFrequency = 1,
					MinimumWordLength = 2,
				});
			return Json(new
				{
					User = user,
					Matches = result
				});
		}

		public object Name(string name)
		{
			var q = Session.Query<User>("Users/Search")
			               .Search(x => x.Name, name);

			var results = q.ToList();

			if (results.Count == 0)
			{
				var suggestionQueryResult = q.Suggest();
				switch (suggestionQueryResult.Suggestions.Length)
				{
					case 0:
						return Json("No idea");
					case 1:
						return Name(suggestionQueryResult.Suggestions[0]);
					default:
						return Json(new
							{
								DidYouMean = suggestionQueryResult.Suggestions
							});
				}
			}

			return Json(results);
		} 
	}
}