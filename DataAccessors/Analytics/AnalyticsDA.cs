using Microsoft.EntityFrameworkCore;


namespace Portfolio.DataAccessors.Analytics
{
	using Configuration.Database;
	using Configuration.Database.Tables.Analytics;
	using Data.Analytics;
	using DataAccessors.Interfaces;
	using Interfaces;


	/// <inheritdoc/>
	public class AnalyticsDA : IAnalyticsDA
	{
		private readonly Context context;


		/// <summary>
		/// Constructor for <see cref="AnalyticsDA"/>
		/// </summary>
		/// <param name="context"></param>
		public AnalyticsDA(Context context)
		{
			this.context = context;
		}


		async Task<bool> ICreate<AnalyticsDTO>.Create(AnalyticsDTO item)
		{
			var eventType = await context.AnalyticsEvents
				.FirstOrDefaultAsync(eventType => eventType.Guid == item.EventType);

			if (eventType == null)
				return false;

			var user = item.User == null
				? null
				: await context.Users.FirstOrDefaultAsync(user => user.Email == item.User);


			var entry = new Analytics
			{
				Event = eventType,
				User = user,
				Created = item.Created,
				MetaData = item.MetaData
			};

			context.Analytics.Add(entry);

			await context.SaveChangesAsync();

			return true;
		}

		IQueryable<AnalyticsDTO> IRead<AnalyticsDTO>.Read()
			=> context.Analytics
				.AsNoTracking()
				.Select(entry => new AnalyticsDTO
				{
					EventType = entry.Event.Guid,
					User = entry.User != null ? entry.User.Email : null,
					Created = entry.Created,
					MetaData = entry.MetaData
				});
	}
}
