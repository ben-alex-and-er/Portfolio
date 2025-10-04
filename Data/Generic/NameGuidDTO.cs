using System.Diagnostics.CodeAnalysis;


namespace Portfolio.Data.Generic
{
	/// <summary>
	/// DTO containing a name and a guid
	/// </summary>
	public class NameGuidDTO
	{
		/// <summary>
		/// Name of object
		/// </summary>
		public required string Name { get; init; }

		/// <summary>
		/// Guid of object
		/// </summary>
		public required Guid Guid { get; init; }


		/// <summary>
		/// Empty Constructor for <see cref="NameGuidDTO"/>
		/// </summary>
		public NameGuidDTO() { }


		/// <summary>
		/// Constructor for <see cref="NameGuidDTO"/>
		/// </summary>
		/// <param name="name"></param>
		/// <param name="guid"></param>
		[SetsRequiredMembers]
		public NameGuidDTO(string name, Guid guid)
		{
			Name = name;
			Guid = guid;
		}
	}
}
