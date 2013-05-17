using System.Linq;
using Courses.RavenDB.London.May2013.Models;
using Raven.Client.Indexes;

namespace Courses.RavenDB.London.May2013.Indexes
{
	public class Dogs_Search : AbstractIndexCreationTask<Dog>
	{
		public Dogs_Search()
		{
			Map = dogs =>
			      from dog in dogs
			      select new
				      {
						  LoadDocument<User>(dog.OwnerId).Active,
					      dog.Name
				      };
		}
	}
}