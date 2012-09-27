using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using OnTimeApi;

namespace APIExample.FormsAuth
{
	[Serializable]
	public class OnTimeIdentity : User, IIdentity
	{
		public string AccessToken { get; private set; }

		public OnTimeIdentity(User user, string accessToken)
		{
			this.id = user.id;
			this.first_name = user.first_name;
			this.last_name = user.last_name;
			this.login_id = user.login_id;

			AccessToken = accessToken;
		}

		public string AuthenticationType
		{
			get
			{
				return "oauth2";
			}
		}
		public bool IsAuthenticated
		{
			get
			{
				return true;
			}
		}
		public string Name
		{
			get
			{
				return login_id;
			}
		}
	}
}