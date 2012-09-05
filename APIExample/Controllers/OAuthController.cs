using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using OnTimeApi;

namespace APIExample.Controllers
{
    public class OAuthController : Controller
    {
		OnTime OnTime = new OnTime(MvcApplication.Settings);

		#region Authorization Code grant type

        public ActionResult ObtainVerificationCode()
        {
			// Redirect end-user to OnTime's auth page
			var settings = MvcApplication.Settings;

			var authUrl = new UriBuilder(settings.OnTimeUrl);
			authUrl.Path += "auth.aspx";

			// Add OAuth2 parameters
			authUrl.Query = string.Format("response_type=code&client_id={0}&redirect_uri={1}&scope=read%20write",
				HttpUtility.UrlEncode(settings.ClientId),
				HttpUtility.UrlEncode(GetRedirectUri())
			);
			
			return Redirect(authUrl.ToString());
        }

		public ActionResult AuthorizationCodeCallback(string code)
		{
			return ObtainAccessToken(new Dictionary<string, string> {
				{ "grant_type", "authorization_code" },
				{ "code", code },
				{ "redirect_uri", GetRedirectUri() },
				{ "scope", "read write" }
			});
		}

		#endregion

		#region Password grant type

		[HttpGet]
		public ActionResult ObtainPassword()
		{
			return View();
		}

		[HttpPost]
		public ActionResult ObtainPassword(string username, string password)
		{
			return ObtainAccessToken(new Dictionary<string, string> {
				{ "grant_type", "password" },
				{ "username", username },
				{ "password", password },
				{ "scope", "read write" }
			});
		}

		#endregion

		private ActionResult ObtainAccessToken(Dictionary<string, string> parameters)
		{
			try
			{
				Session["AccessToken"] = OnTime.ObtainAccessToken(parameters);
				return RedirectToAction("Index", "Home");
			}
			catch(OnTimeException e)
			{
				Response.Write("An error occurred when obtaining access token from OnTime: " + e.Message);
				return null;
			}
		}

		private string GetRedirectUri()
		{
			var redirectUri = new UriBuilder(Request.Url);
			redirectUri.Path = Request.ApplicationPath + "OAuth/AuthorizationCodeCallback";
			redirectUri.Query = null;
			return redirectUri.ToString();
		}
    }
}
