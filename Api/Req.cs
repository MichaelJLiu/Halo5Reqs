using System;
using System.Drawing;
using System.Threading.Tasks;

namespace Halo5Reqs.Api
{
	public class Req
	{
		private static readonly Brush s_rarityBrush = new SolidBrush(Color.FromArgb(204, 255, 255, 255));
		private static readonly Font s_rarityFont = new Font("Segoe UI", 10);

		private Task<Image> _imageTask;

		public Req(
			String id,
			String name,
			String description,
			Int32? sellPrice,
			String rarityId,
			Boolean mythic,
			Int32? levelRequirement,
			String certificationId,
			String imageUrl,
			String smallIconUrl,
			String largeIconUrl,
			String categoryId,
			String subCategoryId,
			String categoryDisplayName)
		{
			this.Id = id;
			this.Name = name;
			this.Description = description;
			this.SellPrice = sellPrice;
			this.Rarity = Rarity.FindById(rarityId);
			this.Mythic = mythic;
			this.LevelRequirement = levelRequirement;
			this.CertificationId = certificationId;
			this.ImageUrl = imageUrl;
			this.SmallIconUrl = smallIconUrl;
			this.LargeIconUrl = largeIconUrl;
			this.Category = ReqCategory.FindById(categoryId);
			this.SubCategory = this.Category.FindSubCategoryById(subCategoryId);
			this.CategoryDisplayName = categoryDisplayName;
		}

		public String Id { get; }

		public String Name { get; }

		public String Description { get; }

		public Int32? SellPrice { get; }

		public Rarity Rarity { get; }

		public Boolean Mythic { get; }

		public Int32? LevelRequirement { get; }

		public String CertificationId { get; }

		public String ImageUrl { get; }

		public String SmallIconUrl { get; }

		public String LargeIconUrl { get; }

		public ReqCategory Category { get; }

		public ReqSubCategory SubCategory { get; }

		public String CategoryDisplayName { get; }

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
			const String mythicBannerImageUrl = "https://content.halocdn.com/media/Default/games/halo-5-guardians/reqs/mythic_banner-c2d7d0b94c4b4a4db890841bfda38cfb.png";
			const String mythicWarzoneBannerImageUrl = "https://content.halocdn.com/media/Default/games/halo-5-guardians/reqs/mythic_warzone_banner-dababb1cf6a64acfbf434891b7bf8d0c.png";

			Image rarityImage = await (this.Mythic ? this.Rarity.GetMythicImageAsync() : this.Rarity.GetImageAsync());
			Image subCategoryImage = await this.SubCategory.GetIconAsync();
			Image mythicBannerImage = this.Mythic
				? await ImageClient.Instance.GetImageAsync(this.Category.Id == "Loadout" || this.Category.Id == "PowerAndVehicle"
					? mythicWarzoneBannerImageUrl
					: mythicBannerImageUrl)
				: null;
			Bitmap bitmap = new Bitmap(312, 432);

			using (Image reqImage = await ImageClient.Instance.LoadImageAsync(this.LargeIconUrl ?? this.ImageUrl))
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.DrawImage(rarityImage, 0, 0, bitmap.Width, bitmap.Height);
				graphics.DrawImage(reqImage, 0, 0, bitmap.Width, bitmap.Height);
				if (subCategoryImage != null)
					graphics.DrawImage(subCategoryImage, bitmap.Width - 72, 48);
				if (mythicBannerImage != null)
					graphics.DrawImageUnscaled(mythicBannerImage, 0, 0);
				graphics.DrawString(this.Rarity.Id.ToUpper(), s_rarityFont, s_rarityBrush, 40, bitmap.Height - 48);
			}

			return bitmap;
		}
	}
}
