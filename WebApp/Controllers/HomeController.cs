using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebApp.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			// check to see whether we already have an access token
			if (User.Identity.IsAuthenticated)
				// we do have an access token - redirect to the Api controller
				return RedirectToAction("Index", "Api");

			// check to see whether Web.config has been updated
			var settings = MvcApplication.Settings;
			
			if (settings.Url == "https://someaccount.axosoft.com/" ||
				settings.ClientId == "00000000-0000-0000-0000-000000000000" ||
				settings.ClientSecret == "00000000-0000-0000-0000-000000000000")
			{
				return View("UpdateConfig");
			}

			return View();
		}

		public ActionResult LogOut()
		{
			FormsAuthentication.SignOut();

			return RedirectToAction("Index");
		}

	}
}
