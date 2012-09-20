using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using OnTimeApi;
using System;
using System.Web;
using APIExample.FormsAuth;
using System.Web.Security;
using System.Security.Principal;

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

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{
			HttpContext context = HttpContext.Current;
		    if (context.Request.IsAuthenticated)
			{
				var loginId = context.User.Identity.Name;
				OnTimeIdentity identity = (OnTimeIdentity)context.Cache["OnTimeUser-" + loginId];
				if(identity == null)
				{
					var accessToken = ((FormsIdentity)User.Identity).Ticket.UserData;
					identity = CacheCurrentUser(accessToken);
				}

				context.User = new GenericPrincipal(identity, new string[]{});
			}
		}

		static public OnTimeIdentity CacheCurrentUser(string accessToken)
		{
			var OnTime = new OnTime(Settings, accessToken);
			var loggedInUser = OnTime.Get<DataResponse<OnTimeApi.User>>("v1/users/me").data;
			var identity = new OnTimeIdentity(loggedInUser, accessToken);
			HttpRuntime.Cache["OnTimeUser-" + loggedInUser.login_id] = identity;

			return identity;
		}
	}
}