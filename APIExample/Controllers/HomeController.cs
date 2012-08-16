using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APIExample.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
			// check to see whether we already have an access token
			var accessToken = (string)Session["AccessToken"];

			if(string.IsNullOrEmpty(accessToken))
				// no access token - redirect to the OAuth controller
				return RedirectToAction("ObtainVerificationCode", "OAuth");

			return View();
        }

		public ActionResult LogOut()
		{
			Session.Remove("AccessToken");

			return null;
		}

		public ActionResult GetProjects()
		{
			var accessToken = (string)Session["AccessToken"];

			// make an API call to OnTime
			var apiCallUrl = new UriBuilder(MvcApplication.Settings.OnTimeUrl);
			apiCallUrl.Path += "api/v1/projects";
			apiCallUrl.Query = "oauth_token=" + accessToken;

			return Redirect(apiCallUrl.ToString());

		}

    }
}
