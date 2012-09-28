using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using OnTimeApi;
using System.Web.Security;

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
			var url = OnTime.GetUrl(resource); //using the URL of the specified resource
			// forward any query parameters (other than resource)
			foreach(string queryElement in Request.QueryString)
				if(queryElement != "resource")
					url += "&" + HttpUtility.UrlEncode(queryElement) + "=" + HttpUtility.UrlEncode(Request.QueryString[queryElement]);
			var request = WebRequest.Create(url); 
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
			return new OnTime(MvcApplication.Settings, ((FormsIdentity)User.Identity).Ticket.UserData /* we are storing the Access Token in the UserData of the ticket */);
		}
    }
}
