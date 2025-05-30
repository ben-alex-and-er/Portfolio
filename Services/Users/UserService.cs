using Microsoft.EntityFrameworkCore;


namespace Portfolio.Services.Users
{
	using Data.Analytics;
	using DataAccessors.Analytics.Interfaces;
	using DataAccessors.Users.Interfaces;
	using Interfaces;


	/// <inheritdoc/>
	public class UserService : IUserService
	{
		private readonly IAnalyticsDA analyticsDA;

		private readonly IUserDA userDA;


		/// <summary>
		/// Constructor for <see cref="UserService"/>
		/// </summary>
		/// <param name="analyticsDA"></param>
		/// <param name="userDA"></param>
		public UserService(IAnalyticsDA analyticsDA, IUserDA userDA)
		{
			this.analyticsDA = analyticsDA;
			this.userDA = userDA;
		}


		async Task<bool> IUserService.ExternalLogin(string email)
		{
			var exists = await userDA.Read()
				.AnyAsync(user => user == email);

			if (!exists)
			{
				var createUser = await userDA.Create(email);

				if (!createUser)
					return false;

				// Could add to more tables


				var addRegisterAnalytics = await analyticsDA.Create(new AnalyticsDTO(AnalyticsEventTypes.REGISTER, email));

				if (!addRegisterAnalytics)
					return false;
			}

			var addLoginAnalytics = await analyticsDA.Create(new AnalyticsDTO(AnalyticsEventTypes.LOGIN, email));

			if (!addLoginAnalytics)
				return false;

			return true;
		}
	}
}
