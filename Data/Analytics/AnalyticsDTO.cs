using System.Diagnostics.CodeAnalysis;


namespace Portfolio.Data.Analytics
{
	/// <summary>
	/// DTO containing details of an analytics record
	/// </summary>
	public class AnalyticsDTO
	{
		/// <summary>
		/// Type of analytics record
		/// </summary>
		public required Guid EventType { get; init; }

		/// <summary>
		/// Associated user
		/// </summary>
		public required string? User { get; init; }

		/// <summary>
		/// Datetime of record
		/// </summary>
		public required DateTime Created { get; init; }

		/// <summary>
		/// Additional metadata
		/// </summary>
		public required string? MetaData { get; init; }


		/// <summary>
		/// Empty constructor for <see cref="AnalyticsDTO"/>
		/// </summary>
		public AnalyticsDTO() { }


		/// <summary>
		/// Constructor for <see cref="AnalyticsDTO"/>
		/// </summary>
		/// <param name="eventType"></param>
		/// <param name="user"></param>
		/// <param name="created"></param>
		/// <param name="metadata"></param>
		[SetsRequiredMembers]
		public AnalyticsDTO(Guid eventType, string? user = null, DateTime? created = null, string? metadata = null)
		{
			EventType = eventType;
			User = user;
			Created = created ?? DateTime.UtcNow;
			MetaData = metadata;
		}
	}
}
