using Microsoft.AspNetCore.Components;


namespace Portfolio.Components.Login
{
	/// <summary>
	/// Represents a Google Sign Out Button object
	/// </summary>
	public partial class GoogleSignOutButton
	{
		/// <summary>
		/// Navigation manager to redirect to google authentication endpoint
		/// </summary>
		[Inject]
		public NavigationManager NavigationManager { get; set; } = default!;


		private void RedirectToGoogle()
			=> NavigationManager.NavigateTo("googleauthentication/logout", forceLoad: true);
	}
}