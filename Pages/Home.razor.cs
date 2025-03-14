using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;


namespace Portfolio.Pages
{
	using Providers.Authentication.Extensions;


	/// <summary>
	/// Represents the home page
	/// </summary>
	public partial class Home : ComponentBase
	{
		/// <summary>
		/// Authentication State Provider
		/// </summary>
		[Inject]
		public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;


		private string? fullName;


		/// <inheritdoc/>
		protected override async Task OnInitializedAsync()
		{
			var state = await AuthenticationStateProvider.GetAuthenticationStateAsync();

			if (state == null)
				return;

			fullName = state.GetName();
		}
	}
}