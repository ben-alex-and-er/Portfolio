using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Portfolio.Controllers.Authentication
{
	using Services.Users.Interfaces;


	/// <summary>
	/// Controller for using the google OAuth2 Tenant
	/// </summary>
	[Route("[controller]")]
	public class GoogleAuthenticationController : Controller
	{
		private const string CONTROLLER_NAME = "GoogleAuthentication";


		private readonly IUserService userService;


		/// <summary>
		/// Constructor for <see cref="GoogleAuthenticationController"/>
		/// </summary>
		/// <param name="userService"></param>
		public GoogleAuthenticationController(IUserService userService)
		{
			this.userService = userService;
		}


		/// <summary>
		/// Calls Google's authentication to login
		/// </summary>
		/// <returns></returns>
		[HttpGet("login")]
		public IActionResult Login()
			=> Challenge(
				new AuthenticationProperties
				{
					RedirectUri = Url.Action(nameof(GoogleResponse), CONTROLLER_NAME)
				},
				GoogleDefaults.AuthenticationScheme);

		/// <summary>
		/// Called from google's authentication and sets the user claims
		/// </summary>
		/// <returns></returns>
		[HttpGet("google-response")]
		public async Task<IActionResult> GoogleResponse()
		{
			// TODO: Add query param to login to display message

			var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			if (!result.Succeeded)
				return Redirect("/login");


			var email = result.Principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

			if (email == null)
				return Redirect("/login");


			var extenalLogin = await userService.ExternalLogin(email);

			if (!extenalLogin)
				return Redirect("/login");


			return Redirect("/home");
		}

		/// <summary>
		/// Logs the user out from the application
		/// </summary>
		/// <returns></returns>
		[HttpGet("logout")]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			return Redirect("/login");
		}
	}
}
