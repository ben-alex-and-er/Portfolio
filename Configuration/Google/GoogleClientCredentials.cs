namespace Portfolio.Configuration.Google
{
	/// <summary>
	/// Credentials for google client authentication
	/// </summary>
	public class GoogleClientCredentials
	{
		private const string KEY = "GOOGLE_CLIENT";

		/// <summary>
		/// Google Client ID
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Google Client Secret
		/// </summary>
		public string Secret { get; set; }

		/// <summary>
		/// Google callback path
		/// </summary>
		public string CallbackPath { get; set; } = "/signin-google";


		/// <summary>
		/// Constructr for <see cref="GoogleClientCredentials"/>
		/// </summary>
		/// <param name="configuration"></param>
		public GoogleClientCredentials(IConfiguration configuration)
		{
			configuration.Bind(KEY, this);
		}
	}
}
