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
		[HttpGet]
		public IEnumerable<string> Get() => Enum.GetNames(typeof(Room));
	}
}
