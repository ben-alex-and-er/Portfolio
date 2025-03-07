using Microsoft.AspNetCore.Components;


namespace Portfolio.Components.Login
{
	/// <summary>
	/// Represents a Google Sign In Button object
	/// </summary>
	public partial class GoogleSignInButton : ComponentBase
	{
		/// <summary>
		/// Navigation manager to redirect to google authentication endpoint
		/// </summary>
		[Inject]
		public NavigationManager NavigationManager { get; set; } = default!;


		private void RedirectToGoogle()
			=> NavigationManager.NavigateTo("googleauthentication/login", forceLoad: true);
	}
}