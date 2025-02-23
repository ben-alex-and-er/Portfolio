using Microsoft.AspNetCore.Components;


namespace Portfolio.Pages
{
	using Data.Api;
	using Requests.Authentication;
	using Services.Api.Interfaces;


	public partial class Login : ComponentBase
	{
		[Inject]
		private IApiCallerService ApiCallerService { get; set; } = default!;

		[Inject]
		private NavigationManager NavigationManager { get; set; } = default!;


		private string username = string.Empty;

		private string password = string.Empty;


		private async Task Submit()
		{
			Console.WriteLine("Submit");
			var request = new LoginRequest(username, password);

			var response = await ApiCallerService.PostAsync<LoginRequest, bool>(AuthenticationEndpoints.LOGIN, request);

			if (response)
				NavigationManager.NavigateTo("home");
		}
	}
}