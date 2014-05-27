using System;
using System.Configuration;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebApp.Helpers;

namespace WebApp
{
	public class MvcApplication : System.Web.HttpApplication
	{
		// global settings used by the API Example
		public static Settings Settings { get; private set; }

		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute
			(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);

		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);

			// Initialize Settings from Web.config
			Settings = new Settings
			(
				url: ConfigurationManager.AppSettings.Get("AxosoftAPI:Url"),
				clientId: ConfigurationManager.AppSettings.Get("AxosoftAPI:ClientId"),
				clientSecret: ConfigurationManager.AppSettings.Get("AxosoftAPI:ClientSecret")
			);
		}
	}
}
