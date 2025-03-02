using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;


namespace Portfolio.Providers.Authentication
{
	/// <summary>
	/// Custom authentication state provider
	/// </summary>
	public class AuthStateProvider : AuthenticationStateProvider
	{
		private ClaimsPrincipal currentUser = new();


		/// <inheritdoc/>
		public override Task<AuthenticationState> GetAuthenticationStateAsync()
			=> Task.FromResult(new AuthenticationState(currentUser));

		/// <summary>
		/// Sets the current authentication state
		/// </summary>
		/// <param name="principal"></param>
		public void SetAuthenticationState(ClaimsPrincipal principal)
		{
			currentUser = principal;

			NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(currentUser)));
		}
	}
}
