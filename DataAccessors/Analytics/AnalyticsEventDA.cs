using Microsoft.EntityFrameworkCore;


namespace Portfolio.DataAccessors.Analytics
{
	using Configuration.Database;
	using Configuration.Database.Tables.Analytics;
	using Data.Generic;
	using DataAccessors.Interfaces;
	using Interfaces;


	/// <inheritdoc/>
	public class AnalyticsEventDA : IAnalyticsEventDA
	{
		private readonly Context context;


		/// <summary>
		/// Constructor for <see cref="AnalyticsEventDA"/>
		/// </summary>
		/// <param name="context"></param>
		public AnalyticsEventDA(Context context)
		{
			this.context = context;
		}


		async Task<bool> ICreate<NameGuidDTO>.Create(NameGuidDTO item)
		{
			var guidExists = await context.AnalyticsEvents
				.Where(eventType => eventType.Guid == item.Guid)
				.AnyAsync();

			if (guidExists)
				return false;

			var nameExists = await context.AnalyticsEvents
				.Where(eventType => eventType.Name == item.Name)
				.AnyAsync();

			if (nameExists)
				return false;


			var entry = new AnalyticsEvent
			{
				Name = item.Name,
				Guid = item.Guid
			};

			context.AnalyticsEvents.Add(entry);

			await context.SaveChangesAsync();

			return true;
		}
	}
}
