using System;

namespace BookingPlatform.Backend.Entities
{
	public class WeeklyDateExclusion : DateExclusion
	{
		public DateTime StartDate { get; set; }
		public int WeeklyRecurrence { get; set; }
	}
}
