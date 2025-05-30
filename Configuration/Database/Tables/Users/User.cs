using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Portfolio.Configuration.Database.Tables.Users
{
	/// <summary>
	/// Represents the user table
	/// </summary>
	public class User
	{
		/// <summary>
		/// Identifier
		/// </summary>
		[Key]
		[Column]
		[Required]
		public int Id { get; set; }

		/// <summary>
		/// Email of the user
		/// </summary>
		[Column]
		[Required]
		public string Email { get; set; }
	}
}
