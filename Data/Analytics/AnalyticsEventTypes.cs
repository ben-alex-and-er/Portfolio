namespace Portfolio.Data.Analytics
{
	/// <summary>
	/// Collection of Analytic Event Type Guids
	/// </summary>
	public static class AnalyticsEventTypes
	{
		/// <summary>
		/// Guid (in string form) for the event where a new user registers (does not include the new user login)
		/// </summary>
		public const string REGISTER_STRING = "4b07c3f3-c9c1-45ae-9c1f-ff64f9dc5cd9";

		/// <summary>
		/// Guid (in string form) for the event where a user logs in
		/// </summary>
		public const string LOGIN_STRING = "0fd9f726-2fd3-4e88-9913-e24d57417439";


		/// <summary>
		/// Guid for the event where a new user registers (does not include the new user login)
		/// </summary>
		public static Guid REGISTER = new(REGISTER_STRING);

		/// <summary>
		/// Guid for the event where a user logs in
		/// </summary>
		public static Guid LOGIN = new(LOGIN_STRING);
	}
}
