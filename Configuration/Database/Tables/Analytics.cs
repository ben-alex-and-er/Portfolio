using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Portfolio.Configuration.Database.Tables.Analytics
{
	using Users;


	/// <summary>
	/// Represents the analytics table
	/// </summary>
	public class Analytics
	{
		/// <summary>
		/// Identifier
		/// </summary>
		[Key]
		[Column]
		[Required]
		public int Id { get; set; }

		/// <summary>
		/// Event type
		/// </summary>
		[Column]
		[Required]
		public AnalyticsEvent Event { get; set; }

		/// <summary>
		/// Associated user
		/// </summary>
		[Column]
		public User? User { get; set; }

		/// <summary>
		/// Date time of event
		/// </summary>
		[Column]
		public DateTime Created { get; set; } = DateTime.UtcNow;

		/// <summary>
		/// Any other information
		/// </summary>
		[Column]
		public string? MetaData { get; set; }

		/// <summary>
		/// Foreign key for <see cref="AnalyticsEvent"/>
		/// </summary>
		[ForeignKey(nameof(Event))]
		public int AnalyticsEventId { get; set; }

		/// <summary>
		/// Foreign key for <see cref="User"/>
		/// </summary>
		[ForeignKey(nameof(User))]
		public int? UserId { get; set; }
	}
}
