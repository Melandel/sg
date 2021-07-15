using BookingAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace BookingAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BookingsController : ControllerBase
	{
		private IBookingRepository _repository;

		public BookingsController(LinkGenerator linkGenerator, IBookingRepository repository) {
			_repository = repository;
		}

		[HttpPost]
		public IActionResult Create([FromBody] Booking booking)
		{
			var status = _repository.Add(booking);
			return status == BookingRepository.AddStatus.SUCCESS ? Ok() : Conflict(
				new LinksDto(
					new LinkDto(
						$"{this.Request.Scheme}://{this.Request.Host.Value}{this.Request.PathBase.Value}/Rooms/{booking.Room}/slots?day={booking.BeginningDate.Date.ToString("O")}",
						"list_available_timeslots",
						"GET"
					)
				)
			);
		}

		[HttpDelete]
		public IActionResult Delete([FromBody] Booking booking)
		{
			var status = _repository.Delete(booking);

			switch (status)
			{
				case BookingRepository.DeleteStatus.SUCCESS:
					return Accepted();
				case BookingRepository.DeleteStatus.NOT_FOUND:
					return NotFound();
				case BookingRepository.DeleteStatus.UNEXPECTED_FAILURE:
				default:
					return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}
	}
}
