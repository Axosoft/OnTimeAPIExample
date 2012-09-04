using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnTimeApi
{
	public class MessageResponse
	{
		public string message;
	}

	public class AuthResponse
	{
		public string access_token;
	}

	public class DataResponse<T>
	{
		public List<T> data;
	}

	public class Item
	{
		public string name;
	}
}