using System.Collections.Generic;

namespace Courses.RavenDB.London.May2013.Models
{
	public class User
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Id { get; set; }
		public List<string> Phones { get; set; }
		public bool Active { get; set; }
		public List<string> Hobbies { get; set; }
		public Dictionary<string, string> Address { get; set; }

		public User()
		{
			Hobbies = new List<string>();
			Phones = new List<string>();
			Address = new Dictionary<string, string>();
		}
	}
}