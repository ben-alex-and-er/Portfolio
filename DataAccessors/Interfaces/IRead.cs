namespace Portfolio.DataAccessors.Interfaces
{
	/// <summary>
	///  Interface for ICRUD method for reading data from the database
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IRead<T>
	{
		/// <summary>
		/// Retrieves an IQueryable of T from the database
		/// </summary>
		/// <returns></returns>
		IQueryable<T> Read();
	}
}
