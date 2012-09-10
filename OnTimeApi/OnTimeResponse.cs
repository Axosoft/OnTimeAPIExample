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
}