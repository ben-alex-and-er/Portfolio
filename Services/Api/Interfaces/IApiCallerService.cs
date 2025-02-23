namespace Portfolio.Services.Api.Interfaces
{
	public interface IApiCallerService
	{
		Task<U?> PostAsync<T, U>(string endpoint, T request);
	}
}
