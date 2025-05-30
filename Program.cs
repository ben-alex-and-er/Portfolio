using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Portfolio.Configuration.Database;
using Portfolio.Configuration.ServiceSetup;
using Portfolio.Data.Api;
using Portfolio.Services.Api;
using Portfolio.Services.Api.Interfaces;


internal class Program
{
	private static void AddServices(IHostApplicationBuilder builder)
	{
		builder.Services
			.AddContextServices()
			.AddGoogleServices()
			.AddUserServices();

		builder.Services.BuildServiceProvider().GetRequiredService<Context>().Database.Migrate();

		builder.Services.AddTransient<IApiCallerService, ApiCallerService>();

		builder.Services.AddSingleton<ApiDomain>();
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