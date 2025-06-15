namespace Portfolio.DataAccessors.Users.Interfaces
{
	using DataAccessors.Interfaces;


	/// <summary>
	/// Data accessor for the user table
	/// </summary>
	public interface IUserDA : ICreate<string>, IRead<string>
	{ }
}
