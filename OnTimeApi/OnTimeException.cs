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

		// Goes through the error response and inner exception, trying to find a message to use for this exception
		private static string GetMessage(ErrorResponse response, WebException innerException)
		{
			// list of candidates for the message
			var possibleMessages = new List<string>();

			// add candidates from the error response
			if(response != null)
			{
				possibleMessages.Add(response.message);
				possibleMessages.Add(response.error_description);
				possibleMessages.Add(response.error);
			}
			// add candidates from the inner exception
			if(innerException != null)
				possibleMessages.Add(innerException.Message);

			// return the first candidate that's not empty
			return possibleMessages.FirstOrDefault(message => !string.IsNullOrEmpty(message));
		}
	}
}
