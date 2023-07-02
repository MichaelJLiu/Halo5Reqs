using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Halo5Reqs.Api
{
	public static class HttpClientExtensions
	{
		private static readonly MediaTypeHeaderValue s_jsonContentTypeHeader =
			new MediaTypeHeaderValue("application/json");
		private static readonly HttpMethod s_patchMethod = new HttpMethod("PATCH");

		public static async Task<JObject> GetJObjectAsync(this HttpClient httpClient, String requestUri)
		{
			String json;

			using (HttpResponseMessage response = await httpClient.GetAsync(requestUri))
			{
				response.EnsureSuccessStatusCode();
				json = await response.Content.ReadAsStringAsync();
			}

			return JObject.Parse(json);
		}

		public static Task<JObject> PatchJObjectAsync(this HttpClient httpClient, String requestUri, JObject payload)
		{
			return httpClient.SendJObjectAsync(s_patchMethod, requestUri, payload);
		}

		public static Task<JObject> PostJObjectAsync(this HttpClient httpClient, String requestUri, JObject payload)
		{
			return httpClient.SendJObjectAsync(HttpMethod.Post, requestUri, payload);
		}

		private static async Task<JObject> SendJObjectAsync(this HttpClient httpClient, HttpMethod method, String requestUri, JObject payload)
		{
			MemoryStream stream = new MemoryStream();
			JsonTextWriter writer = new JsonTextWriter(new StreamWriter(stream));
			payload.WriteTo(writer);
			writer.Flush();
			stream.Position = 0;

			HttpRequestMessage request =
				new HttpRequestMessage(s_patchMethod, requestUri)
				{
					Content =
						new StreamContent(stream)
						{
							Headers = { ContentType = s_jsonContentTypeHeader },
						},
				};

			String json;

			using (HttpResponseMessage response = await httpClient.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();
				json = await response.Content.ReadAsStringAsync();
			}

			return JObject.Parse(json);
		}
	}
}
