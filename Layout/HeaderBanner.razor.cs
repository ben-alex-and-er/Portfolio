using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;


namespace Portfolio.Layout
{
	using Providers.Authentication.Extensions;


	/// <summary>
	/// Represents a Header Banner
	/// </summary>
	public partial class HeaderBanner : ComponentBase
	{
		[Inject]
		private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;


		/// <summary>
		/// Determines whether the sign out button should be shown
		/// </summary>
		[Parameter]
		public bool ShowSignOut { get; set; } = true;


		private bool isAuthenticated;


		/// <inheritdoc/>
		protected override async Task OnInitializedAsync()
		{
			var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

			var email = authState?.GetEmail();

			isAuthenticated = email != null;
		}
	}
}