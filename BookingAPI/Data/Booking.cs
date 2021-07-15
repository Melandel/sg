using System;

namespace BookingAPI.Data
{
	public class Booking
	{
		public Room Room { get; }
		public string GuestName { get; }
		public DateTime BeginningDate { get; }
		public int DurationInHours { get; }
		public Booking(Room room, string guestName, DateTime beginningDate, int durationInHours)
		{
			if (string.IsNullOrWhiteSpace(guestName))
				throw new NullOrWhitespaceGuestNameException();
			if (durationInHours < 1)
				throw new NegativeBookingDurationInHoursException(durationInHours);
			if ((beginningDate + TimeSpan.FromHours(durationInHours)).Date > beginningDate.Date)
				throw new TimeSlotOverlapsNextDayException(beginningDate, durationInHours);
			if (beginningDate.Minute != 0 || beginningDate.Second != 0 || beginningDate.Millisecond != 0)
				throw new BookingBeginningDateNotAnExactHourException(beginningDate);

			Room = room;
			GuestName = guestName;
			BeginningDate = beginningDate;
			DurationInHours = durationInHours;
		}
	}
}
