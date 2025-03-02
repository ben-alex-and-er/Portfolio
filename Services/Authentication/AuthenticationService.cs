using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Requests.Authentication;
using Responses.Authentication;
using Responses.Authentication.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace Portfolio.Services.Authentication
{
	using Api.Interfaces;
	using Data.Api;
	using Data.Security;
	using Interfaces;
	using Providers.Authentication;


	/// <summary>
	/// Service for authentication
	/// </summary>
	public class AuthenticationService : IAuthenticationService
	{
		private readonly AuthStateProvider authStateProvider;

		private readonly ProtectedLocalStorage protectedLocalStorage;

		private readonly IApiCallerService apiCallerService;


		/// <summary>
		/// Constructor for <see cref="AuthenticationService"/>
		/// </summary>
		/// <param name="authenticationStateProvider"></param>
		/// <param name="protectedLocalStorage"></param>
		/// <param name="apiCallerService"></param>
		public AuthenticationService(
			AuthenticationStateProvider authenticationStateProvider,
			ProtectedLocalStorage protectedLocalStorage,
			IApiCallerService apiCallerService)
		{
			this.authStateProvider = (AuthStateProvider)authenticationStateProvider;
			this.protectedLocalStorage = protectedLocalStorage;
			this.apiCallerService = apiCallerService;
		}


		async Task<LoginResponseStatus> IAuthenticationService.Login(LoginRequest request)
		{
			var response = await apiCallerService.PostAsync<LoginRequest, LoginResponse>(AuthenticationEndpoints.LOGIN, request);

			if (response?.Token == null)
				return LoginResponseStatus.FAILED;

			if (response.Status != LoginResponseStatus.SUCCESS)
				return response.Status;


			await protectedLocalStorage.SetAsync(JwtConsts.STORAGE_KEY, response.Token);

			var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(response.Token);

			var principal = CreateClaimsPrincipal(jwtToken.Claims);

			authStateProvider.SetAuthenticationState(principal);

			return LoginResponseStatus.SUCCESS;
		}

		private ClaimsPrincipal CreateClaimsPrincipal(IEnumerable<Claim> claims)
		{
			var identity = new ClaimsIdentity(claims, "Pre-pass");

			var principal = new ClaimsPrincipal(identity);

			return principal;
		}
	}
}
