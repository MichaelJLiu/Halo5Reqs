using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Halo5Reqs.Api
{
	public abstract class ApiClientBase
	{
		private static readonly MediaTypeWithQualityHeaderValue s_jsonAcceptHeader =
			new MediaTypeWithQualityHeaderValue("application/json");

		protected ApiClientBase(String baseAddress, String token, Boolean useCache = false)
		{
			this.HttpClient =
				new HttpClient(useCache ? HttpCacheHandler.Instance : new HttpClientHandler())
				{
					BaseAddress = new Uri(baseAddress),
					DefaultRequestHeaders = { Accept = { s_jsonAcceptHeader } },
				};
			this.HttpClient.DefaultRequestHeaders.Add("X-343-Authorization-Spartan", token);
		}

		protected HttpClient HttpClient { get; }
	}
}
