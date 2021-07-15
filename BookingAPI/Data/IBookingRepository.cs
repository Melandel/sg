using System;
using System.Collections.Generic;

namespace BookingAPI.Data
{
	public interface IBookingRepository
	{
		BookingRepository.AddStatus Add(Booking booking);
		BookingRepository.DeleteStatus Delete(Booking booking);
		IReadOnlyCollection<int> GetAllAvaibleTimeSpotsInTheDay(Room room, DateTime day);
	}
}
