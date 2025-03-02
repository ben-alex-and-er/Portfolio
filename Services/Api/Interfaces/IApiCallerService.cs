namespace Portfolio.Services.Api.Interfaces
{
	/// <summary>
	/// Interface for <see cref="ApiCallerService"/>
	/// </summary>
	public interface IApiCallerService
	{
		/// <summary>
		/// Posts the specified request to the specified endpoint
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="U"></typeparam>
		/// <param name="endpoint"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		Task<U?> PostAsync<T, U>(string endpoint, T request);
	}
}
