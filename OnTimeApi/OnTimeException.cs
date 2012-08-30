using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnTimeApi
{
	public class OnTimeException : Exception
	{
		public OnTimeException(string message) : base(message)
		{
		}
	}
}
