
namespace Portfolio.Services.Analytics.Interfaces
{
	using Data.Analytics;


	/// <summary>
	/// Service for analytics operations
	/// </summary>
	public interface IAnalyticsService
	{
		/// <summary>
		/// Gets a collection of analytics records from a certain date
		/// </summary>
		/// <param name="from"></param>
		/// <returns></returns>
		IQueryable<AnalyticsRecord> GetAnalytics(DateTime from);
	}
}
