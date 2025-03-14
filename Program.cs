using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Portfolio.Configuration.Google;
using Portfolio.Data.Api;
using Portfolio.Services.Api;
using Portfolio.Services.Api.Interfaces;


internal class Program
{
	private static void AddServices(IHostApplicationBuilder builder)
	{
		builder.Services.AddTransient<IApiCallerService, ApiCallerService>();

		builder.Services.AddSingleton<ApiDomain>();

		builder.Services.AddSingleton<GoogleClientCredentials>();

		builder.Services.Configure<CookiePolicyOptions>(options =>
		{
			// Allow cross-site requests
			options.MinimumSameSitePolicy = SameSiteMode.Lax;
		});

		var googleCredentials = builder.Services.BuildServiceProvider()
			.GetRequiredService<GoogleClientCredentials>();

		builder.Services.AddAuthentication(options =>
		{
			options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
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

		app.UseHttpsRedirection();
		app.UseStaticFiles();
		app.UseRouting();

		app.UseAntiforgery();

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllers();
		app.MapBlazorHub();
		app.MapFallbackToPage("/_Host");

		app.MapRazorPages();
		app.Run();
	}
}