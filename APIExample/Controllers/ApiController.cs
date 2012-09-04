using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnTimeApi;
using System.Net;

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

			var webClient = new WebClient();

			var resultString = webClient.DownloadString(OnTime.GetUrl("projects"));
			Response.Write(resultString);
			Response.ContentType = "application/json";
			Response.ContentEncoding = System.Text.Encoding.UTF8;
			Response.Charset = "UTF-8";

			return null;
		}

    }
}
