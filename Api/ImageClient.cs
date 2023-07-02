using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Halo5Reqs.Api
{
	public class ImageClient
	{
		public static readonly ImageClient Instance = new ImageClient();

		private readonly HttpClient _httpClient = new HttpClient(HttpCacheHandler.Instance);

		private readonly Dictionary<String, Image> _images = new Dictionary<String, Image>();

		public async Task<Image> GetImageAsync(String imageUrl)
		{
			if (!_images.TryGetValue(imageUrl, out Image image))
			{
				image = await this.LoadImageAsync(imageUrl);
				_images.Add(imageUrl, image);
			}

			return image;
		}

		public async Task<Image> LoadImageAsync(String imageUrl)
		{
			return Image.FromStream(new MemoryStream(await _httpClient.GetByteArrayAsync(imageUrl)));
		}
	}
}
