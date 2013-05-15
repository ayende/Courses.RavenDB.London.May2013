namespace Courses.RavenDB.London.May2013.Models
{
	public class Dog : IHaveName, IHaveOwner
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string OwnerId { get; set; }
	}
}