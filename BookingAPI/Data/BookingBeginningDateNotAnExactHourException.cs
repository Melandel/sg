using System;
using System.Runtime.Serialization;

namespace BookingAPI.Data
{
	[Serializable]
	internal class BookingBeginningDateNotAnExactHourException : FunctionalException
	{
		public BookingBeginningDateNotAnExactHourException(DateTime dateTime) : base($"{dateTime} is not an exact hour.")
		{
		}
	}
}
