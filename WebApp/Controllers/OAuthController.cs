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
			authUrl.Path += "auth";

			// Add OAuth2 parameters
			authUrl.Query = string.Format("response_type=code&client_id={0}&redirect_uri={1}&scope=read%20write",
				HttpUtility.UrlEncode(settings.ClientId),
				HttpUtility.UrlEncode(GetRedirectUri())
			);
			
			return Redirect(authUrl.ToString());
        }

		public ActionResult AuthorizationCodeCallback(string code, string error)
		{
			if(!string.IsNullOrEmpty(code))
			{
				try
				{
					Session["AccessToken"] = OnTime.ObtainAccessTokenFromAuthorizationCode(
						code: code,
						redirectUri: GetRedirectUri(),
						scope: "read write"
					);
					return RedirectToAction("Index", "Home");
				}
				catch(OnTimeException e)
				{
					Response.Write("An error occurred when obtaining access token from OnTime: " + e.Message);
					return null;
				}
			}
			else
			{
				Response.Write(error);
				return null;
			}
		}

		#endregion

		private string GetRedirectUri()
		{
			var redirectUri = new UriBuilder(Request.Url);
			redirectUri.Path = Request.ApplicationPath + "OAuth/AuthorizationCodeCallback";
			redirectUri.Query = null;
			return redirectUri.ToString();
		}
    }
}
