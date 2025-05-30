namespace Portfolio.DataAccessors.Analytics.Interfaces
{
	using Data.Analytics;
	using DataAccessors.Interfaces;


	/// <summary>
	/// Data accessor for analytics
	/// </summary>
	public interface IAnalyticsDA : ICreate<AnalyticsDTO>, IRead<AnalyticsDTO>
	{ }
}
