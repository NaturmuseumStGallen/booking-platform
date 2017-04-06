using System;

namespace BookingPlatform.Backend.Booking
{
	public class AvailabilityProvider
	{
		public AvailabilityStatus For(DateTime day, DateTime time)
		{
			if (day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday)
			{
				return AvailabilityStatus.NotBookable;
			}

			if (day.Day == 11 && time.Hour == 10)
			{
				return AvailabilityStatus.Booked;
			}

			return AvailabilityStatus.Free;
		}
	}
}
