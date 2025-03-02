using Microsoft.AspNetCore.Components;
using Requests.Authentication;
using Responses.Authentication.Enums;


namespace Portfolio.Pages
{
	using Services.Authentication.Interfaces;


	public partial class Login : ComponentBase
	{
		[Inject]
		private IAuthenticationService AuthenticationService { get; set; } = default!;

		[Inject]
		private NavigationManager NavigationManager { get; set; } = default!;


		private string username = string.Empty;

		private string password = string.Empty;


		private async Task Submit()
		{
			var request = new LoginRequest(username, password);

			var response = await AuthenticationService.Login(request);

			if (response != LoginResponseStatus.SUCCESS)
				return;

			NavigationManager.NavigateTo("home");
		}
	}
}