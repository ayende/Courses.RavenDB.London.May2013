using System.Linq;
using Courses.RavenDB.London.May2013.Models;
using Raven.Abstractions.Data;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Courses.RavenDB.London.May2013.Indexes
{
	public class Users_Search : AbstractIndexCreationTask<User>
	{
		public Users_Search()
		{
			Map = users =>
				  from user in users
				  select new
					  {
						  user.Name,
						  user.Hobbies
					  };
			Index(x => x.Name, FieldIndexing.Analyzed);
			TermVector(x=>x.Hobbies, FieldTermVector.WithPositionsAndOffsets);
			Index(x => x.Hobbies, FieldIndexing.Analyzed);
			Suggestion(x => x.Name, new SuggestionOptions
				{
					Accuracy = 0.6f,
					Distance = StringDistanceTypes.Default
				});
		}
	}
}