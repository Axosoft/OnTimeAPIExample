using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using APIExample.Models;
using System.Configuration;

namespace APIExample
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

			routes.MapRoute(
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
			Settings = new Settings(
				onTimeUrl: ConfigurationManager.AppSettings.Get("OnTimeUrl"),
				clientId: ConfigurationManager.AppSettings.Get("ClientId"),
				clientSecret: ConfigurationManager.AppSettings.Get("ClientSecret")
			);

		}
	}
}