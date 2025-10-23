using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;


namespace Portfolio.Configuration.ServiceSetup
{
	using Google;


	public static partial class ServiceCollectionExtensions
	{
		/// <summary>
		/// Adds google related services for injection
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddGoogleServices(this IServiceCollection services)
		{
			services.AddSingleton<GoogleClientCredentials>();

			services.Configure<CookiePolicyOptions>(options =>
			{
				// Allow cross-site requests
				options.MinimumSameSitePolicy = SameSiteMode.Lax;
			});

			var googleCredentials = services.BuildServiceProvider()
				.GetRequiredService<GoogleClientCredentials>();

			services.AddAuthentication(options =>
			{
				options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			})
			.AddCookie(options =>
			{
				options.Cookie.SameSite = SameSiteMode.Lax;
			})
			.AddGoogle(options =>
			{
				options.ClientId = googleCredentials.Id;
				options.ClientSecret = googleCredentials.Secret;
				options.CallbackPath = googleCredentials.CallbackPath;

				options.Events.OnRemoteFailure = context =>
				{
					Console.WriteLine($"Google authentication failed: {context.Failure?.Message}");
					context.Response.Redirect("/");
					context.HandleResponse();
					return Task.CompletedTask;
				};
			});

			return services;
		}
	}
}
