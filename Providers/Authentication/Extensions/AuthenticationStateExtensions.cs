using DTOs.User;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;


namespace Portfolio.Providers.Authentication.Extensions
{
	/// <summary>
	/// Extension methods for <see cref="AuthenticationState"/>
	/// </summary>
	public static class AuthenticationStateExtensions
	{
		/// <summary>
		/// Retrieves the email from the authentication state
		/// </summary>
		/// <param name="authenticationState"></param>
		/// <returns>The current user's email address</returns>
		public static string? GetEmail(this AuthenticationState authenticationState)
		{
			var claims = authenticationState.GetClaims();

			return claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
		}

		/// <summary>
		/// Retrieves the user's first and last names from the authentication state
		/// </summary>
		/// <param name="authenticationState"></param>
		/// <returns>The current user's first and last name</returns>
		public static FirstLastNameDTO GetNames(this AuthenticationState authenticationState)
		{
			var claims = authenticationState.GetClaims();

			var firstName = claims?.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;

			var lastName = claims?.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value;

			return new FirstLastNameDTO(firstName, lastName);
		}

		/// <summary>
		/// Retrieves the user's full name from the authentication state
		/// </summary>
		/// <param name="authenticationState"></param>
		/// <returns>The current user's given name</returns>
		public static string? GetName(this AuthenticationState authenticationState)
		{
			var claims = authenticationState.GetClaims();

			return claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
		}


		private static IEnumerable<Claim>? GetClaims(this AuthenticationState authenticationState)
		{
			var identity = authenticationState.User.Identities.FirstOrDefault(x => x.IsAuthenticated);

			return identity?.Claims;
		}
	}
}
