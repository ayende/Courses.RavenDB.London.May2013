using System.Collections.Generic;

namespace Courses.RavenDB.London.May2013.Models
{
	public class User
	{
		public string Name { get; set; } 
		public string Id { get; set; }
		public List<string> Phones { get; set; }

		public User()
		{
			Phones = new List<string>();
		}
	}
}