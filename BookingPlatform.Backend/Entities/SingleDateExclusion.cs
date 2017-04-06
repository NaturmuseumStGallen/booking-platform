using System;

namespace BookingPlatform.Backend.Entities
{
	public class SingleDateExclusion : DateExclusion
	{
		public DateTime Date { get; set; }
	}
}
