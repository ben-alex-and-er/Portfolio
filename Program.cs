using Microsoft.AspNetCore.Http.Connections;
using Portfolio;
using Portfolio.Data.Api;
using Portfolio.Services.Api;
using Portfolio.Services.Api.Interfaces;


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

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Error", createScopeForErrors: true);
			app.UseHsts();
		}

		app.UseHttpsRedirection();

		app.UseStaticFiles();
		app.UseRouting();

		app.UseAntiforgery();

		app.MapRazorComponents<App>()
			.AddInteractiveServerRenderMode();

		app.UseStatusCodePagesWithRedirects("/login");

		app.MapBlazorHub(options =>
		{
			options.Transports = HttpTransportType.WebSockets;
			options.AllowStatefulReconnects = true;
		});

		app.MapRazorPages();

		app.Run();
	}
}