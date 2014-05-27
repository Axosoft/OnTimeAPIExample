using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Security;
using AxosoftAPI.NET;
using AxosoftAPI.NET.Helpers;
using AxosoftAPI.NET.Models;

namespace WebApp.Controllers
{
	public class OAuthController : Controller
	{
		private Proxy axosoftProxy;

		public OAuthController()
		{
			axosoftProxy = new Proxy
			{
				Url = MvcApplication.Settings.Url,
				ClientId = MvcApplication.Settings.ClientId,
				ClientSecret = MvcApplication.Settings.ClientSecret
			};
		}

		#region Authorization Code grant type

		public ActionResult ObtainVerificationCode()
		{
			// Redirect end-user to Axosoft's auth page
			var settings = MvcApplication.Settings;

			var authUrl = new UriBuilder(settings.Url);
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
			if (!string.IsNullOrEmpty(code))
			{
				try
				{
					var accessToken = axosoftProxy.ObtainAccessTokenFromAuthorizationCode(
						code: code,
						redirectUri: GetRedirectUri(),
						scope: ScopeEnum.ReadWrite
					);

					// get information about the logged in user
					var loggedInUser = axosoftProxy.Me.Get().Data;
					var identityName = loggedInUser.LoginId; // use the login_id as the FormsIdentity name

					var ticket = new FormsAuthenticationTicket(1, identityName, DateTime.Now, DateTime.Now + FormsAuthentication.Timeout, false, accessToken /* store the access token as the userData */);

					var encryptedTicket = FormsAuthentication.Encrypt(ticket);

					HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
					Response.Cookies.Add(authCookie);

					Response.Redirect(FormsAuthentication.GetRedirectUrl(identityName, false));
				}
				catch (AxosoftAPIException<ErrorResponse> e)
				{
					Response.Write("An error occurred when obtaining access token from Axosoft: " + e.Message);
				}
			}
			else
			{
				Response.Write("Obtaining an access token from Axosoft failed with an error: " + error);
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
