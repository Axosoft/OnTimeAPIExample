using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using OnTimeApi;
using System.Web.Security;

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
					var accessToken = OnTime.ObtainAccessTokenFromAuthorizationCode(
						code: code,
						redirectUri: GetRedirectUri(),
						scope: "read write"
					);
					// get information about the logged in user
					var loggedInUser = OnTime.Get<DataResponse<OnTimeApi.User>>("v1/users/me").data;
					var identityName = loggedInUser.login_id; // use the login_id as the FormsIdentity name

					var ticket = new FormsAuthenticationTicket(1, identityName, DateTime.Now, DateTime.Now + FormsAuthentication.Timeout, false, accessToken /* store the access token as the userData */);

					var encryptedTicket = FormsAuthentication.Encrypt(ticket);

					HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
					Response.Cookies.Add(authCookie);

					Response.Redirect(FormsAuthentication.GetRedirectUrl(identityName, false));
				}
				catch(OnTimeException e)
				{
					Response.Write("An error occurred when obtaining access token from OnTime: " + e.Message);
				}
			}
			else
			{
				Response.Write("Obtaining an access token from OnTime failed with an error: " + error);
			}
			return null;
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
