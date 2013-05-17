using System.Security.Principal;
using Raven.Client.Listeners;
using Raven.Json.Linq;

namespace Courses.RavenDB.London.May2013.Controllers
{
	public class AuditStoreListener : IDocumentStoreListener
	{
		public bool BeforeStore(string key, object entityInstance, RavenJObject metadata, RavenJObject original)
		{
			metadata["Modified-By"] = WindowsIdentity.GetCurrent().Name;

			return false;
		}

		public void AfterStore(string key, object entityInstance, RavenJObject metadata)
		{
		}
	}
}