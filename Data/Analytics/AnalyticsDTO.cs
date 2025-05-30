namespace Portfolio.Data.Analytics
{
	/// <summary>
	/// DTO containing details of an analytics record
	/// </summary>
	/// <param name="EventType">Type of analytics event</param>
	/// <param name="User">Associated user</param>
	/// <param name="Created">Datetime of record</param>
	/// <param name="MetaData">Additional metadata</param>
	public record AnalyticsDTO(Guid EventType, string? User, DateTime Created, string? MetaData)
	{
		/// <summary>
		/// Constructor for <see cref="AnalyticsDTO"/>
		/// </summary>
		/// <param name="eventType"></param>
		/// <param name="user"></param>
		/// <param name="created"></param>
		/// <param name="metadata"></param>
		public AnalyticsDTO(Guid eventType, string? user = null, DateTime? created = null, string? metadata = null)
			: this(eventType, user, created ?? DateTime.UtcNow, metadata) { }
	}
}
