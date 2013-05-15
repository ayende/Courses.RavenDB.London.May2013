using System.Linq;
using Courses.RavenDB.London.May2013.Models;
using Raven.Client.Indexes;

namespace Courses.RavenDB.London.May2013.Indexes
{
	public class Names_Search : AbstractMultiMapIndexCreationTask
	{
		public Names_Search()
		{
			AddMap<User>(users => from user in users
								  select new { user.Name });

			AddMap<Cat>(cats => from cat in cats
								select new { cat.Name });

			AddMap<Dog>(dogs => from dog in dogs
								select new { dog.Name });
		}
	}
}