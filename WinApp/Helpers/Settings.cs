using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WinApp.Helpers
{
	public class Settings
	{
		public string Url { get; private set; }
		public string ClientId { get; private set; }
		public string ClientSecret { get; private set; }

		public Settings(string url, string clientId, string clientSecret)
		{
			Url = url;
			ClientId = clientId;
			ClientSecret = clientSecret;
		}
	}
}
