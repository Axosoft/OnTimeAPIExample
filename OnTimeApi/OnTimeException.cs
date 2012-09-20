using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace OnTimeApi
{
	public class OnTimeException : Exception
	{
		public OnTimeException(ErrorResponse response, WebException innerException = null) : base(GetMessage(response, innerException), innerException)
		{
			
		}

		private static string GetMessage(ErrorResponse response, WebException innerException)
		{
			var possibleMessages = new List<string>();
			if(response != null)
			{
				possibleMessages.Add(response.message);
				possibleMessages.Add(response.error_description);
				possibleMessages.Add(response.error);
			}
			if(innerException != null)
				possibleMessages.Add(innerException.Message);

			return possibleMessages.FirstOrDefault(message => !string.IsNullOrEmpty(message));
		}
	}
}
