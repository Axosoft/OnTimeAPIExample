using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnTimeApi;
using System.Net;
using APIExample.FormsAuth;

namespace APIExample.Controllers
{
	[Authorize]
    public class ApiController : Controller
    {
        public ActionResult Index()
        {
			return View(GetOnTime());
        }

		public ActionResult Get(string resource)
		{
			// make an API call to OnTime
			var OnTime = GetOnTime();

			var webClient = new WebClient();

			var resultString = webClient.DownloadString(OnTime.GetUrl(resource));
			Response.Write(resultString);
			Response.ContentType = "application/json";
			Response.ContentEncoding = System.Text.Encoding.UTF8;
			Response.Charset = "UTF-8";

			return null;
		}

		private OnTime GetOnTime()
		{
			return new OnTime(MvcApplication.Settings, ((OnTimeIdentity)User.Identity).AccessToken);
		}
    }
}
