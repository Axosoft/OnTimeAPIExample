using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace OnTimeApi
{
	public class OnTime
	{
		Settings settings;

		public OnTime(Settings settings)
		{
			this.settings = settings;
		}

		public string ObtainAccessToken(string parameters)
		{
			var tokenUrl = new UriBuilder(settings.OnTimeUrl);
			tokenUrl.Path += "api/v1/auth/oauth2";
			tokenUrl.Query += string.Format("{0}&client_id={1}&client_secret={2}",
				parameters,
				HttpUtility.UrlEncode(settings.ClientId),
				HttpUtility.UrlEncode(settings.ClientSecret)
			);
			var webClient = new WebClient();
			try
			{
				var resultString = webClient.DownloadString(tokenUrl.Uri);
				var result = Deserialize<AuthResponse>(resultString);
				return result.access_token;
			} catch (WebException e)
			{
				var response = DeserializeResponse<MessageResponse>(e.Response.GetResponseStream());
				throw new OnTimeException(response.message);
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
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<T>(content);
		}
	}
}
