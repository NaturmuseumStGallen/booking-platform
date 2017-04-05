using System.Web.Mvc;
using System.Web.Routing;

namespace BookingPlatform
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(name: "Admin", url: "admin/{action}", defaults: new { controller = "Admin" });
			routes.MapRoute(name: "Default", url: "{controller}/{action}", defaults: new { action = "Index" });
		}
	}
}
