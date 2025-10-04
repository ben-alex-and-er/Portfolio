using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Radzen;


namespace Portfolio
{
	using Configuration.Database;
	using Configuration.ServiceSetup;
	using Data.Analytics;
	using Data.Api;
	using Data.Generic;
	using DataAccessors.Analytics.Extensions;
	using DataAccessors.Analytics.Interfaces;
	using Services.Api;
	using Services.Api.Interfaces;


	internal static class Program
	{
		private static void AddServices(IHostApplicationBuilder builder)
		{
			builder.Services
				.AddAnalyticsServices()
				.AddContextServices()
				.AddGoogleServices()
				.AddUserServices();

			var provider = builder.Services.BuildServiceProvider();

			provider.GetRequiredService<Context>().Database.Migrate();

			Task.Run(() => AddEventTypes(provider)).Wait();



			builder.Services.AddTransient<IApiCallerService, ApiCallerService>();

			builder.Services.AddSingleton<ApiDomain>();
		}

		private static async Task AddEventTypes(IServiceProvider provider)
		{
			var analyticsEventDA = provider.GetRequiredService<IAnalyticsEventDA>();

			await analyticsEventDA.CreateEventTypeOrThrow(new NameGuidDTO(nameof(AnalyticsEventTypes.REGISTER), AnalyticsEventTypes.REGISTER));
			await analyticsEventDA.CreateEventTypeOrThrow(new NameGuidDTO(nameof(AnalyticsEventTypes.LOGIN), AnalyticsEventTypes.LOGIN));
		}

		private static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddRazorComponents()
				.AddInteractiveServerComponents();

			builder.Services.AddHttpClient();

			AddServices(builder);

			builder.Services.AddRazorPages();
			builder.Services.AddServerSideBlazor();
			builder.Services.AddRadzenComponents();


			var app = builder.Build();

			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseStaticFiles();
			app.UseRouting();

			app.UseAntiforgery();

			app.UseForwardedHeaders(new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
			});

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();
			app.MapBlazorHub();
			app.MapFallbackToPage("/_Host");

			app.MapRazorPages();
			app.Run();
		}
	}
}