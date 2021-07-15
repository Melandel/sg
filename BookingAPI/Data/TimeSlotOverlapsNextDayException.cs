using System;
using System.Runtime.Serialization;

namespace BookingAPI.Data
{
	[Serializable]
	internal class TimeSlotOverlapsNextDayException : FunctionalException
	{
		public TimeSlotOverlapsNextDayException(DateTime beginningDate, int durationInHours):
			base($"TimeSlot starting at {beginningDate.ToString("O")} for {durationInHours} hours overlaps with the next day and is illegal.") { }
	}
}
