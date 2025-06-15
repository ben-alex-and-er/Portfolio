namespace Portfolio.DataAccessors.Analytics.Interfaces
{
	using Data.Generic;
	using DataAccessors.Interfaces;


	/// <summary>
	/// Data accessor for analytics event types
	/// </summary>
	public interface IAnalyticsEventDA : ICreate<NameGuidDTO>, IRead<NameGuidDTO>
	{ }
}
