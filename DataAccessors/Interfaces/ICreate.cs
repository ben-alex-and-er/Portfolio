namespace Portfolio.DataAccessors.Interfaces
{
	/// <summary>
	/// Interface for ICRUD method for creating entries in the database
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface ICreate<T>
	{
		/// <summary>
		/// Adds a new entry to the database
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		Task<bool> Create(T item);
	}
}
