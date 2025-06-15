namespace Portfolio.DataAccessors.Analytics.Interfaces
{
	using Configuration.Database.Tables.Analytics;
	using Data.Analytics;
	using DataAccessors.Interfaces;


	/// <summary>
	/// Data accessor for analytics
	/// </summary>
	public interface IAnalyticsDA : ICreate<AnalyticsDTO>, IRead<AnalyticsDTO>
	{
		IQueryable<Analytics> ReadPure();
	}
}
