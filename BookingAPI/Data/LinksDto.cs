using System.Collections.Generic;

namespace BookingAPI.Data
{
	public class LinksDto
	{
		public IReadOnlyCollection<LinkDto> Links { get; }

		public LinksDto(params LinkDto[] links) {
			Links = links;
		}
	}
}

