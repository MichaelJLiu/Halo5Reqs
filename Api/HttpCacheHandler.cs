using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Halo5Reqs.Api
{
	public class HttpCacheHandler : DelegatingHandler
	{
		public static readonly HttpMessageHandler Instance =
			new HttpCacheHandler($"\\\\?\\{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\Halo5Reqs\\Cache");

		private readonly String _cacheDirectory;

		public HttpCacheHandler(String cacheDirectory)
			: base(new HttpClientHandler())
		{
			_cacheDirectory = cacheDirectory;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			String cacheFilePath = Path.Combine(
				_cacheDirectory,
				request.RequestUri.GetComponents(UriComponents.Host | UriComponents.Path, UriFormat.Unescaped).Replace("/", "\\"));

			if (File.Exists(cacheFilePath))
			{
				return
					new HttpResponseMessage(HttpStatusCode.OK)
					{
						Content = new StreamContent(File.OpenRead(cacheFilePath))
					};
			}

			String cacheDirectory = Path.GetDirectoryName(cacheFilePath);
			HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

			if (response.StatusCode == HttpStatusCode.OK)
			{
				Directory.CreateDirectory(Path.GetDirectoryName(cacheFilePath));
				File.WriteAllBytes(cacheFilePath, await response.Content.ReadAsByteArrayAsync());
			}

			return response;
		}
	}
}
