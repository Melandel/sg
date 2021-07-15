using System;
using System.Runtime.Serialization;

namespace BookingAPI.Data
{
	[Serializable]
	internal class NullOrWhitespaceGuestNameException : FunctionalException
	{
		public NullOrWhitespaceGuestNameException():
			base($"Guest name is empty.") { }

	}
}
