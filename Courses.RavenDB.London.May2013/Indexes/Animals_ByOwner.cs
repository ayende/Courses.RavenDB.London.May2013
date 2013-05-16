using System.Linq;
using Courses.RavenDB.London.May2013.Models;
using Raven.Client.Indexes;

namespace Courses.RavenDB.London.May2013.Indexes
{
	public class Animals_ByOwner : AbstractMultiMapIndexCreationTask
	{
		public Animals_ByOwner()
		{
			AddMap<Cat>(cats => from cat in cats
			                    select new {cat.OwnerId});

			AddMap<Dog>(dogs => from dog in dogs
								select new { dog.OwnerId });


		}
	}
}