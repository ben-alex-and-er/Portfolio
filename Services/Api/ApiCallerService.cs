using System.Text.Json;


namespace Portfolio.Services.Api
{
	using Data.Api;
	using Interfaces;

	
	/// <summary>
	/// Service which calls endpoints on the api
	/// </summary>
	public class ApiCallerService : IApiCallerService
	{
		private readonly HttpClient httpClient;

		private readonly string domain;

		private readonly JsonSerializerOptions options;


		/// <summary>
		/// Constructor for <see cref="ApiCallerService"/>
		/// </summary>
		/// <param name="httpClient"></param>
		/// <param name="apiDomain"></param>
		public ApiCallerService(HttpClient httpClient, ApiDomain apiDomain)
		{
			this.httpClient = httpClient;
			domain = apiDomain.Domain;

			options = new JsonSerializerOptions()
			{
				PropertyNameCaseInsensitive = true
			};
		}


		async Task<U?> IApiCallerService.PostAsync<T, U>(string endpoint, T request) where U : default
		{
			var url = $"{domain}/{endpoint}";

			var post = await httpClient.PostAsJsonAsync(url, request);

			if (!post.IsSuccessStatusCode)
				throw new HttpRequestException($"Request failed with status code: {post.StatusCode}");

			var response = await post.Content.ReadAsStringAsync();

			var deserialize = JsonSerializer.Deserialize<U>(response, options);

			return deserialize;
		}
	}
}
