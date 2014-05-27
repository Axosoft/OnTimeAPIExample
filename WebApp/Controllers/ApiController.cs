using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AxosoftAPI.NET;

namespace WebApp.Controllers
{
	[Authorize]
	public class ApiController : Controller
	{
		public ActionResult Index()
		{
			return View(GetAxosoftProxy());
		}

		public ActionResult Proxy(string resource)
		{
			// make an API call to Axosoft
			var axosoftProxy = GetAxosoftProxy();

			// forward any query parameters (other than resource)
			var parameters = new Dictionary<string, object>();
			foreach (string queryElement in Request.QueryString)
			{
				if (queryElement != "resource")
				{
					parameters.Add(queryElement, Request.QueryString[queryElement]);
				}
			}

			// create the HTTP request
			var request = axosoftProxy.BuildRequest(resource, parameters); //using the URL of the specified resource
			request.Method = Request.HttpMethod; // use the same method that was used to make the proxy call

			if (string.Compare(Request.HttpMethod, "POST", ignoreCase: true) == 0)
			{
				request.ContentType = Request.ContentType; // and the same content type
				Request.InputStream.Seek(0, SeekOrigin.Begin);
				Request.InputStream.CopyTo(request.GetRequestStream()); // and the same payload
			}

			// make the request and grab the response
			HttpWebResponse response;
			try
			{
				response = (HttpWebResponse)request.GetResponse();
			}
			catch (WebException e)
			{
				response = (HttpWebResponse)e.Response;
			}

			// proxy the results back to our caller
			var resultStream = response.GetResponseStream();

			Response.ContentType = response.ContentType;
			try
			{
				Response.ContentEncoding = Encoding.GetEncoding(response.ContentEncoding);
			}
			catch { }

			Response.Charset = response.CharacterSet;
			Response.StatusCode = (int)response.StatusCode;
			Response.StatusDescription = response.StatusDescription;

			resultStream.CopyTo(Response.OutputStream);

			return null;
		}

		private Proxy GetAxosoftProxy()
		{
			return new Proxy
			{
				Url = MvcApplication.Settings.Url,
				ClientId = MvcApplication.Settings.ClientId,
				ClientSecret = MvcApplication.Settings.ClientSecret,
				/* we are storing the Access Token in the UserData of the ticket */
				AccessToken = ((FormsIdentity)User.Identity).Ticket.UserData
			};
		}
	}
}
