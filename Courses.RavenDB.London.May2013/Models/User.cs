using System.Collections.Generic;

namespace Courses.RavenDB.London.May2013.Models
{
	public class User : IHaveName
	{
		public string Name { get; set; }
		public string Email { get; set; } 
		public string Id { get; set; }
		public List<string> Phones { get; set; }
		public List<Hobby>  Hobbies { get; set; }
		public Dictionary<string, string> Address { get; set; }

		public User()
		{
			Hobbies = new List<Hobby>();
			Phones = new List<string>();
			Address = new Dictionary<string, string>();
		}

		public class Hobby
		{
			public string Name { get; set; }
			public bool Dangerous { get; set; }
		}
	}
}