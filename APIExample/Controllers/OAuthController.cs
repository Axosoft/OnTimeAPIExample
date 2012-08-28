using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using APIExample.Models;

namespace APIExample.Controllers
{
    public class OAuthController : Controller
    {
		#region Authorization Code grant type

        public ActionResult ObtainVerificationCode()
        {
			// Redirect end-user to OnTime's auth page
			var settings = MvcApplication.Settings;

			var authUrl = new UriBuilder(settings.OnTimeUrl);
			authUrl.Path += "auth.aspx";

			// Add OAuth2 parameters
			authUrl.Query = string.Format("response_type=code&client_id={0}&redirect_uri={1}",
				HttpUtility.UrlEncode(settings.ClientId),
				HttpUtility.UrlEncode(GetRedirectUri())
			);
			
			return Redirect(authUrl.ToString());
        }

		public ActionResult AuthorizationCodeCallback(string code)
		{
			return ObtainAccessToken("grant_type=authorization_code&code=" + HttpUtility.UrlEncode(code));
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
			return ObtainAccessToken(string.Format("grant_type=password&username={0}&password={1}",
				HttpUtility.UrlEncode(username),
				HttpUtility.UrlEncode(password)));
		}

		#endregion

		private ActionResult ObtainAccessToken(string parameters)
		{
			var settings = MvcApplication.Settings;

			var tokenUrl = new UriBuilder(settings.OnTimeUrl);
			tokenUrl.Path += "api/v1/auth/oauth2";
			tokenUrl.Query += string.Format("{0}&client_id={1}&client_secret={2}&redirect_uri={3}",
				parameters,
				HttpUtility.UrlEncode(settings.ClientId),
				HttpUtility.UrlEncode(settings.ClientSecret),
				HttpUtility.UrlEncode(GetRedirectUri())
			);
			var webClient = new WebClient();
			try
			{
				var resultString = webClient.DownloadString(tokenUrl.Uri);
				var result = Deserialize<AuthResponse>(resultString);
				Session["AccessToken"] = result.access_token;

				return RedirectToAction("Index", "Home");
			} catch (WebException e)
			{
				var response = DeserializeResponse<MessageResponse>(e.Response.GetResponseStream());

				Response.Write("An error occurred when obtaining access token from OnTime: " + response.message);
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

		private T DeserializeResponse<T>(Stream stream)
		{
			var reader = new StreamReader(stream, System.Text.Encoding.GetEncoding("utf-8"));
			string content = reader.ReadToEnd();
			stream.Close();

			return Deserialize<T>(content);
		}

		private T Deserialize<T>(string content)
		{
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<T>(content);
		}
    }
}
