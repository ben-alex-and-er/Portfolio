using Microsoft.EntityFrameworkCore;


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


		IReadOnlyList<AnalyticsRecord> IAnalyticsService.GetAnalytics(DateTime from, DateTime to, Func<DateTime, DateTime> timeIncrements, Func<DateTime, long> bucketDesignate)
		{
			var bucket = new SortedDictionary<long, AnalyticsRecord>();

			var data = analyticsDA.ReadPure()
				.Where(entry => entry.Created > from)
				.Where(entry => entry.Created < to)
				.AsSingleQuery();

			foreach (var entry in data)
			{
				var key = bucketDesignate(entry.Created);

				if (!bucket.TryGetValue(key, out var record))
				{
					record = bucket[key] = new AnalyticsRecord
					{
						Date = entry.Created
					};
				}

				// This ONLY works if the event ID is correct
				_ = entry.AnalyticsEventId switch
				{
					1 => record.Registers++,
					2 => record.Logins++,
					_ => 0
				};
			}

			return BackfillMissingData(bucket, from, to, timeIncrements, bucketDesignate);
		}

		async Task<DateTime> IAnalyticsService.GetFirstAnalyticsDate()
			=> (await analyticsDA.ReadPure()
				.OrderBy(x => x.Created)
				.FirstAsync()).Created;


		private static IReadOnlyList<AnalyticsRecord> BackfillMissingData(
			SortedDictionary<long, AnalyticsRecord> bucket,
			DateTime from,
			DateTime to,
			Func<DateTime, DateTime> timeIncrements,
			Func<DateTime, long> bucketDesignate)
		{
			var capacity = 0;

			for (DateTime current = from; current <= to; current = timeIncrements(current))
			{
				capacity++;
			}

			var result = new AnalyticsRecord[capacity];

			var dateTime = from;

			for (int i = 0; i < result.Length; i++)
			{
				var key = bucketDesignate(dateTime);

				var record = bucket.TryGetValue(key, out var value)
					? value
					: new AnalyticsRecord
					{
						Date = dateTime
					};

				result[i] = record;

				dateTime = timeIncrements(dateTime);
			}

			return result;
		}
	}
}
