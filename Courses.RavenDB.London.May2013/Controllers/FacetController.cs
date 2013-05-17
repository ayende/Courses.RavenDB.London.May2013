using Courses.RavenDB.London.May2013.Models;
using Newtonsoft.Json;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Json.Linq;
using Raven.Client.Linq;

namespace Courses.RavenDB.London.May2013.Controllers
{
	public class FacetController : RavenController
	{
		 public object Facet(string q)
		 {
			 var f = Session.Advanced.LuceneQuery<Dog>("Dogs/Search")
			        .Where(q)
			        .ToFacets(new Facet[]
				        {
					        new Facet<Dog>
						        {
							        Name = x => x.Gender,
						        },
					        new Facet<Dog>
						        {
							        Name = x => x.Color
						        }
				        });
			 var s = JsonConvert.SerializeObject(f, Formatting.Indented, new JsonSerializerSettings
				 {
					 NullValueHandling = NullValueHandling.Ignore
				 });
			 return Content(s);
		 }

		 public object Aggregate()
		 {
			 var f = Session.Query<Dog>("Dogs/Search")
			                .Where(x => x.NextVisit > 2 && x.NextVisit < 8)
			                .AggregateBy(x => x.Gender)
								.MaxOn(x => x.Age)
			                .ToList();
			 var s = JsonConvert.SerializeObject(f, Formatting.Indented, new JsonSerializerSettings
			 {
				 NullValueHandling = NullValueHandling.Ignore
			 });
			 return Content(s);
		 }
	}

}