using Microsoft.EntityFrameworkCore;


namespace Portfolio.DataAccessors.Analytics.Extensions
{
	using Data.Generic;
	using Interfaces;


	/// <summary>
	/// Extension methods for <see cref="IAnalyticsEventDA"/>
	/// </summary>
	public static class AnalyticsEventDAExtensions
	{
		/// <summary>
		/// Determines whether an analytics event type already exists
		/// </summary>
		/// <param name="da"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		public static Task<bool> Exists(this IAnalyticsEventDA da, NameGuidDTO item)
			=> da.Read()
				.Where(entry => entry.Name == item.Name)
				.Where(entry => entry.Guid == item.Guid)
				.AnyAsync();


		/// <summary>
		/// Attempts to create an analytics event type if it does not exist. Throws if the creation fails
		/// </summary>
		/// <param name="analyticsEventDA"></param>
		/// <param name="eventType"></param>
		/// <returns></returns>
		/// <exception cref="DbUpdateException"></exception>
		public static async Task CreateEventTypeOrThrow(this IAnalyticsEventDA analyticsEventDA, NameGuidDTO eventType)
		{
			if (await analyticsEventDA.Exists(eventType))
				return;

			var create = await analyticsEventDA.Create(eventType);

			if (!create)
				throw new DbUpdateException($"Failed to create Analytics Event Type: {eventType.Name} ({eventType.Guid})");
		}
	}
}
