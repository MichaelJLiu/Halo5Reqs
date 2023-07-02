using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace Halo5Reqs.Api
{
	public class PackType
	{
		private Task<Image> _imageTask;

		public PackType(
			String id,
			String name,
			String description,
			String iconUrl,
			Int32? creditPrice,
			String backgroundImageUrl,
			String flairImageUrl,
			String giftBannerUrl,
			String giftIconUrl,
			List<PackInstance> instances)
		{
			this.Id = id;
			this.Name = name;
			this.Description = description;
			this.IconUrl = iconUrl;
			this.CreditPrice = creditPrice;
			this.BackgroundImageUrl = backgroundImageUrl;
			this.FlairImageUrl = flairImageUrl;
			this.GiftBannerUrl = giftBannerUrl;
			this.GiftIconUrl = giftIconUrl;
			this.Instances = instances;
		}

		public String Id { get; }

		public String Name { get; }

		public String Description { get; }

		public String IconUrl { get; }

		public Int32? CreditPrice { get; }

		public String BackgroundImageUrl { get; }

		public String FlairImageUrl { get; set; }

		public String GiftBannerUrl { get; set; }

		public String GiftIconUrl { get; set; }

		public List<PackInstance> Instances { get; }

		public Task<Image> GetImageAsync()
		{
			if (_imageTask == null)
			{
				_imageTask = this.CreateImageAsync();
			}

			return _imageTask;
		}

		public override String ToString()
		{
			return this.Name;
		}

		private async Task<Image> CreateImageAsync()
		{
			Image backgroundImage = await ImageClient.Instance.GetImageAsync(this.BackgroundImageUrl);
			Bitmap bitmap = new Bitmap(336, 439);

			using (Image packImage = await ImageClient.Instance.LoadImageAsync(this.IconUrl))
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				int scaledHeight = bitmap.Width * packImage.Height / packImage.Width;
				graphics.DrawImage(backgroundImage, 0, 0, bitmap.Width, bitmap.Height);
				graphics.DrawImage(packImage, 0, (bitmap.Height - scaledHeight) / 2, bitmap.Width, scaledHeight);
			}

			return bitmap;
		}
	}
}
