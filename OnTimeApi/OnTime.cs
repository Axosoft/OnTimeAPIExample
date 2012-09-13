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
		public Settings Settings { get; private set; }
		// the access token given by the OnTime API to grant access to resources
		string accessToken;

		public OnTime(Settings settings, string accessToken = null)
		{
			Settings = settings;
			this.accessToken = accessToken;
		}

		public bool HasAccessToken()
		{
			return accessToken != null;
		}

		#region Obtaining an Access Token

		/// <summary>
		/// Obtains an access token using an authorization code that was returned by the authorization enpoint.
		/// </summary>
		/// <param name="code">The authorization code returned by the authorization endpont.</param>
		/// <param name="redirectUri">The same redirect uri that was passed to the authrization endpoint.</param>
		/// <param name="scope">The requested scope.</param>
		/// <returns>The access token.</returns>
		public string ObtainAccessTokenFromAuthorizationCode(string code, string redirectUri, string scope)
		{
			return ObtainAccessToken(new Dictionary<string, object> {
				{ "grant_type", "authorization_code" },
				{ "code", code },
				{ "redirect_uri", redirectUri },
				{ "scope", scope }
			});
		}

		/// <summary>
		/// Obtains an access token using the user's username and password.
		/// </summary>
		/// <param name="username">The user's username.</param>
		/// <param name="password">The user's password.</param>
		/// <param name="scope">The requested scope.</param>
		/// <returns>The access token.</returns>
		public string ObtainAccessTokenFromUsernamePassword(string username, string password, string scope)
		{
			return ObtainAccessToken(new Dictionary<string, object> {
				{ "grant_type", "password" },
				{ "username", username },
				{ "password", password },
				{ "scope", scope }
			});
		}

		/// <summary>
		/// Helper function used by ObtainAccessTokenFromAuthorizationCode and ObtainAccessTokenFromUsernamePassword
		/// </summary>
		private string ObtainAccessToken(IEnumerable<KeyValuePair<string, object>> parameters)
		{
			// add client id and client secret to the parameters
			parameters = parameters.Concat(new Dictionary<string,object> {
				{ "client_id", Settings.ClientId },
				{ "client_secret", Settings.ClientSecret }
			});
			
			// get the response from the token endpoint
			var authResponse = Get<AuthResponse>("auth/oauth2", parameters);
			
			// store and return access token
			accessToken = authResponse.access_token;
			return authResponse.access_token;
		}

		#endregion

		#region GET / POST helpers

		/// <summary>
		/// Issues a GET request to a resource URL, and returns the deserialized response.
		/// </summary>
		/// <typeparam name="ResponseT">The type into which to deserialize the response.</typeparam>
		/// <param name="resource">The resource (e.g. "projects" or "auth/oauth2" used in constructing the URL for the call.</param>
		/// <param name="parameters">Any additional parameters to be used in the query.</param>
		/// <returns></returns>
		public ResponseT Get<ResponseT>(string resource, IEnumerable<KeyValuePair<string, object>> parameters = null)
		{
		    var request = WebRequest.Create(GetUrl(resource, parameters));
			return MakeRequest<ResponseT>(request);
		}

		/// <summary>
		/// Issues a POST request to a resource URL, and returns the deserialized response.
		/// </summary>
		/// <typeparam name="ResponseT">The type into which to deserialize the response.</typeparam>
		/// <param name="resource">The resource (e.g. "defects") used in constructing the URL for the call.</param>
		/// <param name="content">The content to be posted.</param>
		public ResponseT Post<ResponseT>(string resource, object content, IEnumerable<KeyValuePair<string, object>> parameters = null)
		{
			var request = WebRequest.Create(GetUrl(resource, parameters));
			request.Method = "POST";

			var requestStream = request.GetRequestStream();
			if(content is Stream)
			{
				request.ContentType = "application/octet-stream";
				((Stream)content).CopyTo(requestStream);
			}
			else
			{
				var encoding = new System.Text.UTF8Encoding();

				request.ContentType = "application/json";
				request.Headers.Add("Content-Encoding", encoding.HeaderName);

				var bytes = encoding.GetBytes(JsonConvert.SerializeObject(content));
				requestStream.Write(bytes, 0, bytes.Length);
			}			

			return MakeRequest<ResponseT>(request);
		}

		/// <summary>
		/// Isses a DELETE request to a resource URL
		/// </summary>
		/// <param name="resource">The resource (e.g. "defects") used in constructing the URL for the call.</param>
		/// <param name="id">The id of the resource to be deleted.</param>
		public void Delete(string resource, int id)
		{
		    var request = WebRequest.Create(GetUrl(resource + "/" + id));
			request.Method = "DELETE";

			MakeRequest<object>(request);
		}

		#endregion

		/// <summary>
		/// Gets the URL for an API resource, using the base OnTime URL from settings, the current API version,
		/// the obtained access token (if available), and specified parameters
		/// </summary>
		/// <param name="resource">The resource for which the URL is requested, e.g. "projects", or "auth/oauth2"</param>
		/// <param name="parameters">Optional list of key/value parameters to be added to the query</param>
		/// <returns>The full URL for the resource.</returns>
		public string GetUrl(string resource, IEnumerable<KeyValuePair<string, object>> parameters = null)
		{
			var apiCallUrl = new UriBuilder(Settings.OnTimeUrl);
			apiCallUrl.Path += "api/v1/" + resource;
			
			var finalParameters = new Dictionary<string, string>();
			
			if(parameters != null)
				foreach(var parameter in parameters)
					finalParameters.Add(parameter.Key, parameter.Value.ToString());

			if(accessToken != null)
				finalParameters.Add("oauth_token", accessToken);

			apiCallUrl.Query = string.Join(
				"&",
				(from parameter in finalParameters select (parameter.Key + "=" + HttpUtility.UrlEncode(parameter.Value))).ToArray()
			);

			return apiCallUrl.ToString();
		}

		private T MakeRequest<T>(WebRequest request)
		{
			try
			{
				var response = request.GetResponse();
				var responseStream = response.GetResponseStream();

				return DeserializeResponse<T>(responseStream);
			} catch (WebException e)
			{
				MessageResponse response = null;
				if(e.Response != null)
					try
					{
						response = DeserializeResponse<MessageResponse>(e.Response.GetResponseStream());
					}
					catch(Exception)
					{}
				throw new OnTimeException(response != null ? response.message : null, e);
			}
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
