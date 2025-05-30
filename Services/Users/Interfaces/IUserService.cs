namespace Portfolio.Services.Users.Interfaces
{
	/// <summary>
	/// User related service for manipulating the database
	/// </summary>
	public interface IUserService
	{
		/// <summary>
		/// Attempts to add a user to the database
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		Task<bool> TryAddUser(string email);
	}
}
