using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingAPI.Data
{

	public class BookingRepository : IBookingRepository
	{
		private static readonly Dictionary<Room, Dictionary<DateTime, Dictionary<int, string>>> _bookingsByRoom = new Dictionary<Room, Dictionary<DateTime, Dictionary<int, string>>>();
		private static object _addLock = new Object();
		private static object _deleteLock = new Object();

		static BookingRepository()
		{
			foreach (var room in (Room[])Enum.GetValues(typeof(Room)))
				_bookingsByRoom[room] = new Dictionary<DateTime, Dictionary<int, string>>();
		}

		public enum AddStatus { SUCCESS, CONFLICT }
		public AddStatus Add(Booking booking)
		{
			lock (_addLock)
			{
				for (int i = 0; i < booking.DurationInHours; i++)
				{
					var timeSlotBeginningTime = booking.BeginningDate.Hour + i;

					if (!_bookingsByRoom[booking.Room].ContainsKey(booking.BeginningDate.Date))
						continue;

					if (_bookingsByRoom[booking.Room][booking.BeginningDate.Date].TryGetValue(timeSlotBeginningTime, out string _))
						return AddStatus.CONFLICT;
				}

				for (int i = 0; i < booking.DurationInHours; i++)
				{
					var timeSlotBeginningTime = booking.BeginningDate.Hour + i;
					if (!_bookingsByRoom[booking.Room].ContainsKey(booking.BeginningDate.Date))
						_bookingsByRoom[booking.Room][booking.BeginningDate.Date] = new Dictionary<int, string>();

					_bookingsByRoom[booking.Room][booking.BeginningDate.Date][timeSlotBeginningTime] = booking.GuestName;
				}
				return AddStatus.SUCCESS;
			}
		}

		public enum DeleteStatus { SUCCESS, NOT_FOUND, UNEXPECTED_FAILURE }
		public DeleteStatus Delete(Booking booking)
		{
			lock (_deleteLock)
			{
				try
				{
					if (booking.BeginningDate.Hour > 0 && _bookingsByRoom[booking.Room][booking.BeginningDate.Date].TryGetValue(booking.BeginningDate.Hour - 1, out string _))
						return DeleteStatus.NOT_FOUND;

					if (booking.BeginningDate.Hour < 24 && _bookingsByRoom[booking.Room][booking.BeginningDate.Date].TryGetValue(booking.BeginningDate.Hour + booking.DurationInHours + 1, out string _))
						return DeleteStatus.NOT_FOUND;

					for (int i = 0; i < booking.DurationInHours; i++)
					{
						var timeSlotBeginningTime = booking.BeginningDate.Hour + i;

						if (!_bookingsByRoom[booking.Room].ContainsKey(booking.BeginningDate.Date))
							continue;

						if (!_bookingsByRoom[booking.Room][booking.BeginningDate.Date].TryGetValue(timeSlotBeginningTime, out string _))
							return DeleteStatus.NOT_FOUND;
					}

					for (int i = 0; i < booking.DurationInHours; i++)
					{
						var timeSlotBeginningTime = booking.BeginningDate.Hour + i;
						if (!_bookingsByRoom[booking.Room].ContainsKey(booking.BeginningDate.Date))
							_bookingsByRoom[booking.Room][booking.BeginningDate.Date] = new Dictionary<int, string>();

						_bookingsByRoom[booking.Room][booking.BeginningDate.Date].Remove(timeSlotBeginningTime);
					}

					return DeleteStatus.SUCCESS;
				}
				catch (Exception)
				{
					return DeleteStatus.UNEXPECTED_FAILURE;
				}
			}
		}


		public IReadOnlyCollection<int> GetAllAvaibleTimeSpotsInTheDay(Room room, DateTime day)
		{
			var bookingsForThatRoom = _bookingsByRoom[room];
			if (!bookingsForThatRoom.ContainsKey(day.Date))
				return Enumerable.Range(0, 24).ToList();

			var bookingsForThatDay = bookingsForThatRoom[day.Date];
			return Enumerable.Range(0, 24).Except(bookingsForThatDay.Keys).ToList();
		}
	}
}
