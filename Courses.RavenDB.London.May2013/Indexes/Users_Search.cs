using System.Linq;
using Courses.RavenDB.London.May2013.Models;
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
						Query = new object[]
							{
								user.Name,
								user.Name.Split(' '),
								user.Email.Split('@'),
								user.Email
							}
				      };
		}
	}
}