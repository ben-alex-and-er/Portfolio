using Portfolio.Data.Api;
using Portfolio.Services.Api;
using Portfolio.Services.Api.Interfaces;
using Portfolio.Services.Authentication;
using Portfolio.Services.Authentication.Interfaces;


internal class Program
{
	private static void AddServices(IHostApplicationBuilder builder)
	{
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

		app.UseHttpsRedirection();
		app.UseStaticFiles();
		app.UseRouting();

		app.UseAntiforgery();

		app.MapBlazorHub();
		app.MapFallbackToPage("/_Host");

		app.MapRazorPages();
		app.Run();
	}
}