namespace Portfolio.Configuration.ServiceSetup
{
	using DataAccessors.Analytics;
	using DataAccessors.Analytics.Interfaces;
	using Services.Analytics;
	using Services.Analytics.Interfaces;


	public static partial class ServiceCollectionExtensions
	{
		/// <summary>
		/// Adds analytics related services for injection
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddAnalyticsServices(this IServiceCollection services)
		{
			services.AddSingleton<AnalyticsBackgroundService>();
			services.AddHostedService(services => services.GetRequiredService<AnalyticsBackgroundService>());

			services.AddTransient<IAnalyticsService, AnalyticsService>();

			services.AddTransient<IAnalyticsDA, AnalyticsDA>();
			services.AddTransient<IAnalyticsEventDA, AnalyticsEventDA>();

			return services;
		}
	}
}
