using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.Data
{
	public class FunctionalException : Exception
	{
		public FunctionalException(string message): base(message)
		{
		}
	}
}
