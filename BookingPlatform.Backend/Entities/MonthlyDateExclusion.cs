using System;

namespace BookingPlatform.Backend.Entities
{
	public class MonthlyDateExclusion : DateExclusion
	{
		public DateTime StartDate { get; set; }
		public int MonthlyRecurrence { get; set; }
		public int DayOfWeek { get; set; }
		public int WeekOfMonth { get; set; }
	}
}
