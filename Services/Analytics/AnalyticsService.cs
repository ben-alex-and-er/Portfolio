namespace Portfolio.Services.Analytics
{
	using Data.Analytics;
	using DataAccessors.Analytics.Interfaces;
	using Interfaces;


	/// <inheritdoc/>
	public class AnalyticsService : IAnalyticsService
	{
		private readonly IAnalyticsDA analyticsDA;


		/// <summary>
		/// Constructor for <see cref="AnalyticsService"/>
		/// </summary>
		/// <param name="analyticsDA"></param>
		public AnalyticsService(IAnalyticsDA analyticsDA)
		{
			this.analyticsDA = analyticsDA;
		}


		// TODO: Optimise and make more generic
		IQueryable<AnalyticsRecord> IAnalyticsService.GetAnalytics(DateTime from)
			=> analyticsDA.Read()
				.Where(entry => entry.Created > from)
				.GroupBy(entry => entry.Created.Date)
				.Select(group => new AnalyticsRecord
				{
					Date = group.Key,
					Registers = group.Count(entry => entry.EventType == AnalyticsEventTypes.REGISTER),
					Logins = group.Count(entry => entry.EventType == AnalyticsEventTypes.LOGIN)
				})
				.OrderBy(record => record.Date);
	}
}
