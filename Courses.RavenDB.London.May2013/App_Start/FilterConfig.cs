using System.Web;
using System.Web.Mvc;

namespace Courses.RavenDB.London.May2013
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}