namespace Portfolio.Data.Generic
{
	/// <summary>
	/// DTO containing a name and a guid
	/// </summary>
	/// <param name="Name">Name of object</param>
	/// <param name="Guid">Guid for object</param>
	public record NameGuidDTO(string Name, Guid Guid);
}
