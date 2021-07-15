using System.Text.Json.Serialization;

namespace BookingAPI.Data
{
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum Room { Room0, Room1, Room2, Room3, Room4, Room5, Room6, Room7, Room8, Room9 };
}
