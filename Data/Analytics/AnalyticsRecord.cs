namespace Portfolio.Data.Analytics
{
	/// <summary>
	/// Record for analytics for a certain Date
	/// </summary>
	public class AnalyticsRecord
	{
		/// <summary>
		/// Date of analytics events
		/// </summary>
		public DateTime Date { get; init; }

		/// <summary>
		/// Total count of register events
		/// </summary>
		public int Registers { get; init; }

		/// <summary>
		/// Total count of login events
		/// </summary>
		public int Logins { get; init; }
	}
}
