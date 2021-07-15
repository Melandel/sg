using System;
using System.Runtime.Serialization;

namespace BookingAPI.Data
{
	[Serializable]
	internal class NegativeBookingDurationInHoursException : FunctionalException
	{
		public NegativeBookingDurationInHoursException(int duration) : base($"Booking duration {duration} is negative.") { }
	}
}
