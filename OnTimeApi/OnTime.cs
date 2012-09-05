using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace OnTimeApi
{
	
	/// <summary>
	/// Helper class to access the OnTime API.  Provides simple synchronous helpers for GET and POST API calls.
	/// </summary>
	public class OnTime
	{
		// settings such as the OnTime URL, client id and secret
		Settings settings;
		// the access token given by the OnTime API to grant access to resources
		string accessToken;

		public OnTime(Settings settings, string accessToken = null)
		{
			this.settings = settings;
			this.accessToken = accessToken;
		}

		public string ObtainAccessTokenFromAuthorizationCode(string code, string redirectUri, string scope)
		{
			return ObtainAccessToken(new Dictionary<string, string> {
				{ "grant_type", "authorization_code" },
				{ "code", code },
				{ "redirect_uri", redirectUri },
				{ "scope", scope }
			});
		}

		public string ObtainAccessTokenFromUsernamePassword(string username, string password, string scope)
		{
			return ObtainAccessToken(new Dictionary<string, string> {
				{ "grant_type", "password" },
				{ "username", username },
				{ "password", password },
				{ "scope", scope }
			});
		}

		private string ObtainAccessToken(IEnumerable<KeyValuePair<string, string>> parameters)
		{
			parameters = parameters.Concat(new Dictionary<string,string> {
				{ "client_id", settings.ClientId },
				{ "client_secret", settings.ClientSecret }
			});
			
			var authResponse = Get<AuthResponse>("auth/oauth2", parameters);
			
			accessToken = authResponse.access_token;
			return authResponse.access_token;
		}

		#region GET / POST helpers

		public ResponseT Get<ResponseT>(string resource, IEnumerable<KeyValuePair<string, string>> parameters = null)
		{
			var webClient = new WebClient();
			try
			{
				var resultString = webClient.DownloadString(GetUrl(resource, parameters));
				return Deserialize<ResponseT>(resultString);
			} catch (WebException e)
			{
				MessageResponse response = null;
				if(e.Response != null)
					response = DeserializeResponse<MessageResponse>(e.Response.GetResponseStream());
				throw new OnTimeException(response != null ? response.message : null, e);
			}
		}

		public void Post(string resource, object content, IEnumerable<KeyValuePair<string, string>> parameters = null)
		{
			var webClient = new WebClient();
			var encoding = new System.Text.UTF8Encoding();
			webClient.Encoding = encoding;
			webClient.Headers.Add("Content-Type","application/json");

			var response = webClient.UploadData(GetUrl(resource, parameters), encoding.GetBytes(JsonConvert.SerializeObject(content)));
		}


		#endregion

		public string GetUrl(string apiCall, IEnumerable<KeyValuePair<string, string>> parameters = null)
		{
			var apiCallUrl = new UriBuilder(settings.OnTimeUrl);
			apiCallUrl.Path += "api/v1/" + apiCall;
			
			var finalParameters = new Dictionary<string, string>();
			
			if(parameters != null)
				foreach(var parameter in parameters)
					finalParameters.Add(parameter.Key, parameter.Value);

			if(accessToken != null)
				finalParameters.Add("oauth_token", accessToken);

			apiCallUrl.Query = string.Join(
				"&",
				(from parameter in finalParameters select (parameter.Key + "=" + HttpUtility.UrlEncode(parameter.Value))).ToArray()
			);

			return apiCallUrl.ToString();
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
			return JsonConvert.DeserializeObject<T>(content);
		}
	}
}
