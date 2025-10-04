using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Portfolio.Configuration.Database.Tables.Analytics
{
	/// <summary>
	/// Represents the analytics event table
	/// </summary>
	public class AnalyticsEvent
	{
		/// <summary>
		/// Identifier
		/// </summary>
		[Key]
		[Column]
		[Required]
		public int Id { get; set; }

		/// <summary>
		/// Name of event type
		/// </summary>
		[Column]
		[Required]
		public string Name { get; set; }

		/// <summary>
		/// Guid of event type
		/// </summary>
		[Column]
		[Required]
		public Guid Guid { get; set; }
	}
}
