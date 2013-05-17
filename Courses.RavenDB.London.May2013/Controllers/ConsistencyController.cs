using System.Collections.Concurrent;
using System.Net.Http;
using System.Web;
using Courses.RavenDB.London.May2013.Models;
using System.Linq;
using Raven.Abstractions.Data;
using Raven.Client.Util;

namespace Courses.RavenDB.London.May2013.Controllers
{
	public class ConsistencyController : RavenController
	{
		public object Add()
		{
			Session.Store(new Dog());

			return Json("ok");
		}

		public object List()
		{
			var q = Session.Query<Dog>()
			.Customize(x => x.WaitForNonStaleResultsAsOfLastWrite())
						   .ToList();
			return Json(q);
		}
	}

	public class PerUserEtagHolder : ILastEtagHolder
	{
		readonly ConcurrentDictionary<string, Etag> etagPerUser = new ConcurrentDictionary<string, Etag>();

		public void UpdateLastWrittenEtag(Etag etag)
		{
			var s = HttpContext.Current.Request.QueryString["user"];
			if (s == null)
				return;

			etagPerUser.AddOrUpdate(s, etag, (s1, etag1) => etag);
		}

		public Etag GetLastWrittenEtag()
		{
			var s = HttpContext.Current.Request.QueryString["user"];
			if (s == null)
				return null;
			Etag etag;
			etagPerUser.TryGetValue(s, out etag);
			return etag ?? Etag.Empty;
		}
	}
}