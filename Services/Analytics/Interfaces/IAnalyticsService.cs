
namespace Portfolio.Services.Analytics.Interfaces
{
	using Data.Analytics;


	/// <summary>
	/// Service for analytics operations
	/// </summary>
	public interface IAnalyticsService
	{
		/// <summary>
		/// Gets a collection of analytics records between a date range
		/// </summary>
		/// <param name="from">Filter from</param>
		/// <param name="to">Filter to</param>
		/// <param name="timeIncrements">Time increments between X values</param>
		/// <param name="bucketDesignate">Function for grouping datetimes into X values</param>
		/// <returns></returns>
		IReadOnlyList<AnalyticsRecord> GetAnalytics(DateTime from, DateTime to, Func<DateTime, DateTime> timeIncrements, Func<DateTime, long> bucketDesignate);

		/// <summary>
		/// Retrieves the date of the first analytics event
		/// </summary>
		/// <returns></returns>
		Task<DateTime> GetFirstAnalyticsDate();
	}
}
