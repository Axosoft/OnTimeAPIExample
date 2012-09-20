using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnTimeApi
{

	public class ErrorResponse
	{
		public string error;
		public string error_description;
		public string message;
	}

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
		public T data;
	}

	public class Item
	{
		public int id { get; set; }
		public string name  { get; set; }
		public Project project  { get; set; }
	}

	public class Project
	{
		public int id { get; set; }
		public string name { get; set; }

		public override string ToString()
		{
			return name;
		}
	}

	public class User
	{
		public int id { get; set; }
		public string first_name { get; set; }
		public string last_name { get; set; }
		public string login_id { get; set; }
	}
}