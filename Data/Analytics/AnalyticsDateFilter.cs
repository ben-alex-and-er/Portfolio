namespace Portfolio.Data.Analytics
{
	/// <summary>
	/// Date filter type for analytics
	/// </summary>
	public enum AnalyticsDateFilter
	{
		/// <summary>
		/// Filtered by the last day
		/// </summary>
		LAST_DAY = 0,

		/// <summary>
		/// Filtered by the last week
		/// </summary>
		LAST_WEEK = 1,

		/// <summary>
		/// Filtered by the last month
		/// </summary>
		LAST_MONTH = 2,

		/// <summary>
		/// Filtered by the last year
		/// </summary>
		LAST_YEAR = 3,

		/// <summary>
		/// Filtered by all time (no time filter)
		/// </summary>
		ALL_TIME = 4
	}
}
