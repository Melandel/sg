using System;
using System.Collections.Generic;
using BookingAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookingAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class RoomsController : ControllerBase
	{
		private IBookingRepository _repository;

		public RoomsController(IBookingRepository repository)	{
			_repository = repository;
		}

		[HttpGet]
		public IEnumerable<string> Get() => Enum.GetNames(typeof(Room));

		[HttpGet]
		[Route("{room}/slots")]
		public IEnumerable<int> Slots(Room room, DateTime day) => _repository.GetAllAvaibleTimeSpotsInTheDay(room, day);
	}
}
