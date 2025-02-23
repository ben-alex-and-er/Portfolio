namespace Portfolio.Data.Api
{
	/// <summary>
	/// Data object containing the domain for the API
	/// </summary>
	public class ApiDomain
	{
		private const string KEY = "ApiDomain";


		/// <summary>
		/// API Domain
		/// </summary>
		public string Domain { get; set; } = string.Empty;


		/// <summary>
		/// Constructor for <see cref="ApiDomain"/>
		/// </summary>
		/// <param name="configuration"></param>
		public ApiDomain(IConfiguration configuration)
		{
			configuration.Bind(KEY, this);
		}
	}
}
