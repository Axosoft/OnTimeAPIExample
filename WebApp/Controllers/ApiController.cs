using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnTimeApi;
using System.Net;
using APIExample.FormsAuth;
using System.IO;
using System.Text;

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

			var request = WebRequest.Create(OnTime.GetUrl(resource));

			Stream resultStream;
			HttpWebResponse response;
			try
			{
				response = (HttpWebResponse)request.GetResponse();
			} catch (WebException e)
			{
				response = (HttpWebResponse)e.Response;
			}

			resultStream = response.GetResponseStream();
			
			Response.ContentType = response.ContentType;
			try
			{
				Response.ContentEncoding = Encoding.GetEncoding(response.ContentEncoding);
			} catch(Exception) {}
			Response.Charset = response.CharacterSet;
			Response.StatusCode = (int)response.StatusCode;
			Response.StatusDescription = response.StatusDescription;
			
			resultStream.CopyTo(Response.OutputStream);
			
			return null;
		}

		private OnTime GetOnTime()
		{
			return new OnTime(MvcApplication.Settings, ((OnTimeIdentity)User.Identity).AccessToken);
		}
    }
}
