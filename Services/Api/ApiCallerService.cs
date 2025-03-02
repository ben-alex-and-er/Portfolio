using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Net.Http.Headers;
using System.Text.Json;


namespace Portfolio.Services.Api
{
	using Data.Api;
	using Data.Security;
	using Interfaces;


	/// <summary>
	/// Service which calls endpoints on the api
	/// </summary>
	public class ApiCallerService : IApiCallerService
	{
		private readonly HttpClient httpClient;

		private readonly string domain;

		private readonly ProtectedLocalStorage protectedLocalStorage;

		private readonly JsonSerializerOptions options;


		/// <summary>
		/// Constructor for <see cref="ApiCallerService"/>
		/// </summary>
		/// <param name="httpClient"></param>
		/// <param name="apiDomain"></param>
		/// <param name="protectedLocalStorage"></param>
		public ApiCallerService(
			HttpClient httpClient,
			ApiDomain apiDomain,
			ProtectedLocalStorage protectedLocalStorage)
		{
			this.httpClient = httpClient;
			domain = apiDomain.Domain;
			this.protectedLocalStorage = protectedLocalStorage;

			options = new JsonSerializerOptions()
			{
				PropertyNameCaseInsensitive = true
			};
		}


		async Task<U?> IApiCallerService.PostAsync<T, U>(string endpoint, T request) where U : default
		{
			var url = $"{domain}/{endpoint}";

			var jwtToken = await protectedLocalStorage.GetAsync<string>(JwtConsts.STORAGE_KEY);

			if (jwtToken.Success)
			{
				var header = new AuthenticationHeaderValue(JwtConsts.BEARER_SCHEME, jwtToken.Value);

				httpClient.DefaultRequestHeaders.Authorization = header;
			}

			var post = await httpClient.PostAsJsonAsync(url, request);

			if (!post.IsSuccessStatusCode)
				throw new HttpRequestException($"Request failed with status code: {post.StatusCode}");

			var response = await post.Content.ReadAsStringAsync();

			var deserialize = JsonSerializer.Deserialize<U>(response, options);

			return deserialize;
		}
	}
}
