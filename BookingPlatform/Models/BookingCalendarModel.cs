using System;
using System.Collections.Generic;
using BookingPlatform.Backend.Booking;

namespace BookingPlatform.Models
{
	public class BookingCalendarModel
	{
		public IList<DateTime> Days { get; set; }
		public IList<DateTime> Times { get; set; }
		public AvailabilityProvider Availability { get; set; }
	}
}