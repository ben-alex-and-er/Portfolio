namespace Portfolio.Configuration.ServiceSetup
{
	using Database;


	public static partial class ServiceCollectionExtensions
	{
		/// <summary>
		/// Adds context related services for injection
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddContextServices(this IServiceCollection services)
		{
			services.AddDbContext<Context>();

			return services;
		}
	}
}
