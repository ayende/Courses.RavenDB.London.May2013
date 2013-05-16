using System;
using Courses.RavenDB.London.May2013.Indexes;
using Courses.RavenDB.London.May2013.Models;
using System.Linq;
using Raven.Client;

namespace Courses.RavenDB.London.May2013.Controllers
{
	public class SearchController : RavenController
	{
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