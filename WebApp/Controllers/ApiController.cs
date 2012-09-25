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

		public ActionResult Proxy(string resource)
		{
			// make an API call to OnTime
			var OnTime = GetOnTime();

			// create the HTTP request
			var request = WebRequest.Create(OnTime.GetUrl(resource)); //using the URL of the specified resource
			request.Method = Request.HttpMethod; // use the same method that was used to make the proxy call
			if(string.Compare(Request.HttpMethod, "POST", ignoreCase: true) == 0)
			{
				request.ContentType = Request.ContentType; // and the same content type
				Request.InputStream.Seek(0, SeekOrigin.Begin);
				Request.InputStream.CopyTo(request.GetRequestStream()); // and the same payload
			}
			
			// make the request and grab the response
			Stream resultStream;
			HttpWebResponse response;
			try
			{
				response = (HttpWebResponse)request.GetResponse();
			} catch (WebException e)
			{
				response = (HttpWebResponse)e.Response;
			}

			// proxy the results back to our caller
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
