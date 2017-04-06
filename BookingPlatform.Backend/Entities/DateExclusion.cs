namespace BookingPlatform.Backend.Entities
{
	public abstract class DateExclusion
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool IsWholeDay { get; set; }
	}
}
