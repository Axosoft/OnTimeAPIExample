using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnTimeApi;

namespace APIExample.Controllers
{
    public class ApiController : Controller
    {
        //
        // GET: /Api/

        public ActionResult Index()
        {
			var OnTime = new OnTime(MvcApplication.Settings, (string)Session["AccessToken"]);
            return View(OnTime);
        }

		public ActionResult GetProjects()
		{
			// make an API call to OnTime
			var OnTime = new OnTime(MvcApplication.Settings, (string)Session["AccessToken"]);
			return Redirect(OnTime.GetUrl("projects"));
		}

    }
}
