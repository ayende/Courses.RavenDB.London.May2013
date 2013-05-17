using System;
using System.Web.Mvc;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;

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
						DefaultDatabase = "facets"
					};
				store.RegisterListener(new AuditStoreListener());
				store.Initialize();

				store.LastEtagHolder = new PerUserEtagHolder();

				IndexCreation.CreateIndexes(
					typeof (UsersController).Assembly,
					store);

				return store;
			});

		public new IDocumentSession Session { get; set; }

		protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
		{
			return base.Json(data, contentType, contentEncoding, JsonRequestBehavior.AllowGet);
		}

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