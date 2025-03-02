namespace Portfolio.Data.Security
{
	/// <summary>
	/// Constants related to Jwt
	/// </summary>
	public static class JwtConsts
	{
		/// <summary>
		/// Key for local protected storage
		/// </summary>
		public const string STORAGE_KEY = "Jwt";

		/// <summary>
		/// The scheme for storing the jwt header
		/// </summary>
		public const string BEARER_SCHEME = "Bearer";
	}
}
