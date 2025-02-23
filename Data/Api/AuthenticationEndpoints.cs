namespace Portfolio.Data.Api
{
	/// <summary>
	/// Endpoints for the Authentication Controller
	/// </summary>
	public static class AuthenticationEndpoints
	{
		private const string CONTROLLER = "authentication";

		/// <summary>
		/// Location of the Add User endpoint
		/// </summary>
		public const string ADD_USER = $"{CONTROLLER}/adduser";

		/// <summary>
		/// Location of the Login endpoint
		/// </summary>
		public const string LOGIN = $"{CONTROLLER}/login";
	}
}
