using System;
using System.Web.Mvc;
using Raven.Client;
using Raven.Client.Document;

namespace Courses.RavenDB.London.May2013.Controllers
{
	public class RavenController : Controller
	{
		public static IDocumentStore DocumentStore { get { return documentStore.Value; }}

		public static readonly Lazy<IDocumentStore> documentStore = new Lazy<IDocumentStore>(() =>
			{
				var store = new DocumentStore
					{
						Url = "http://localhost:8080",
						DefaultDatabase = "Courses"
					};
				store.Initialize();

				return store;
			});

		public new IDocumentSession Session { get; set; }

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			Session = DocumentStore.OpenSession();
		}

		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			using (Session)
			{
				if (Session == null)
					return;
				if (filterContext.Exception != null)
					return;
				Session.SaveChanges();
			}
		}

	}
}