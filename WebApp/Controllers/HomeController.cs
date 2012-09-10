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

			if(!string.IsNullOrEmpty(accessToken))
				// we do have an access token - redirect to the Api controller
				return RedirectToAction("Index", "Api");

			return View();
        }

		public ActionResult LogOut()
		{
			Session.Remove("AccessToken");

			return RedirectToAction("Index");
		}

    }
}
