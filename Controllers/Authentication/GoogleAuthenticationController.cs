using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;


namespace Portfolio.Controllers.Authentication
{
	/// <summary>
	/// Controller for using the google OAuth2 Tenant
	/// </summary>
	[Route("[controller]")]
	public class GoogleAuthenticationController : Controller
	{
		private const string CONTROLLER_NAME = "GoogleAuthentication";


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
			var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			if (!result.Succeeded)
				return Redirect("/login");

			// TODO: Use result.Principal to get name, and add claims

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
