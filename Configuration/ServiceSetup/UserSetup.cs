namespace Portfolio.Configuration.ServiceSetup
{
	using DataAccessors.Users;
	using DataAccessors.Users.Interfaces;
	using Services.Users;
	using Services.Users.Interfaces;


	public static partial class ServiceCollectionExtensions
	{
		/// <summary>
		/// Adds user related services for injection
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddUserServices(this IServiceCollection services)
		{
			services.AddTransient<IUserService, UserService>();
			services.AddTransient<IUserDA, UserDA>();

			return services;
		}
	}
}
