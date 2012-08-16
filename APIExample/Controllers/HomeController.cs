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
			// Redirect end-user to OnTime's auth page
			var authUrl = new UriBuilder(MvcApplication.Settings.OnTimeUrl);
			authUrl.Path += "auth.aspx";
			return Redirect(authUrl.ToString());
        }

    }
}
