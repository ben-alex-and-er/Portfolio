using Requests.Authentication;
using Responses.Authentication.Enums;


namespace Portfolio.Services.Authentication.Interfaces
{
	/// <summary>
	/// Interface for <see cref="AuthenticationService"/>
	/// </summary>
	public interface IAuthenticationService
	{
		/// <summary>
		/// Logs the user in, and returns whether it was successful
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		Task<LoginResponseStatus> Login(LoginRequest request);
	}
}
