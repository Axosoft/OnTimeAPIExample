using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APIExample.Controllers
{
    public class ApiController : Controller
    {
        //
        // GET: /Api/

        public ActionResult Index()
        {
            return View();
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
