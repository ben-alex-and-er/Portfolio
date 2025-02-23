using System.Text.Json;


namespace Portfolio.Services.Api
{
	using Data.Api;
	using Interfaces;


	public class ApiCallerService : IApiCallerService
	{
		private readonly HttpClient httpClient;

		private readonly string domain;


		public ApiCallerService(HttpClient httpClient, ApiDomain apiDomain)
		{
			this.httpClient = httpClient;
			domain = apiDomain.Domain;
		}


		async Task<U?> IApiCallerService.PostAsync<T, U>(string endpoint, T request) where U : default
		{
			var url = $"{domain}/{endpoint}";

			var post = await httpClient.PostAsJsonAsync(url, request);

			if (!post.IsSuccessStatusCode)
				throw new HttpRequestException($"Request failed with status code: {post.StatusCode}");

			var response = await post.Content.ReadAsStringAsync();

			var deserialize = JsonSerializer.Deserialize<U>(response);

			return deserialize;
		}
	}
}
