namespace Portfolio.Providers.Analytics
{
	/// <summary>
	/// Extensions for analytics
	/// </summary>
	public static class AnalyticsExtensions
	{
		// This is generated once then can be indexed later
		private static readonly Dictionary<int, string> ordinalIndicators = Enumerable
			.Range(1, 31)
			.ToDictionary(
				day => day,
				day => $"{day}{GetOrdinalIndicator(day)}"
			);

		/// <summary>
		/// Gets the day with the ordinal indicator e.g. 1 returns "1st", 2 returns "2nd"
		/// </summary>
		/// <param name="day"></param>
		/// <returns></returns>
		public static string WithOrdinalIndicator(this int day)
			=> ordinalIndicators[day];

		/// <summary>
		/// Gets the ordinal indicator for a number e.g. 1 returns "st", 2 returns "nd"
		/// </summary>
		/// <param name="number"></param>
		/// <returns></returns>
		public static string GetOrdinalIndicator(int number) => (number % 100) switch
		{
			11 or 12 or 13 => "th",
			_ => (number % 10) switch
			{
				1 => "st",
				2 => "nd",
				3 => "rd",
				_ => "th"
			}
		};
	}
}
