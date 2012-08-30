using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnTimeApi
{
	public class Settings
	{
		public string OnTimeUrl { get; private set; }
		public string ClientId { get; private set; }
		public string ClientSecret { get; private set; }

		public Settings(string onTimeUrl, string clientId, string clientSecret)
		{
			OnTimeUrl = onTimeUrl;
			ClientId = clientId;
			ClientSecret = clientSecret;
		}

	}
}
