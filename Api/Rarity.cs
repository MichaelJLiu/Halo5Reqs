using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Halo5Reqs.Api
{
	public class Rarity
	{
		private static readonly Rarity[] s_rarities =
		{
			new Rarity(0, "Common",
				"https://content.halocdn.com/media/Default/games/halo-5-guardians/req-backgrounds/durable-common-5056ddd0a784449290257c4197da178b.png"),
			new Rarity(1, "Uncommon",
				"https://content.halocdn.com/media/Default/games/halo-5-guardians/req-backgrounds/durable-uncommon-1abf306928e04297be0f88639c74bcb2.png"),
			new Rarity(2, "Rare",
				"https://content.halocdn.com/media/Default/games/halo-5-guardians/req-backgrounds/durable-rare-4e93071b5ae34bf98c6169cdc96739ee.png"),
			new Rarity(3, "UltraRare",
				"https://content.halocdn.com/media/Default/games/halo-5-guardians/req-backgrounds/durable-ultra-rare-df11410dbff8445985e71942e0f8bc98.png",
				"https://content.halocdn.com/media/Default/games/halo-5-guardians/req-backgrounds/durable-ultra-rare-mythic-64e10094eb5746e19c1a615f2ed2b134.png"),
			new Rarity(4, "Legendary",
				"https://content.halocdn.com/media/Default/games/halo-5-guardians/req-backgrounds/durable-legendary-23ef7602cba840d7899f109cbfad896b.png",
				"https://content.halocdn.com/media/Default/games/halo-5-guardians/req-backgrounds/durable-legendary-mythic-64ae95bf09c54a48b02d8404c89244f2.png"),
		};

		private static readonly Dictionary<String, Rarity> s_raritiesById =
			s_rarities.ToDictionary(rarity => rarity.Id);

		public static Rarity FindById(String id) => s_raritiesById[id];

		private Image _image;
		private Image _mythicImage;

		public Rarity(Int32 order, String id, String imageUrl = null, String mythicImageUrl = null)
		{
			this.Id = id;
			this.Order = order;
			this.ImageUrl = imageUrl;
			this.MythicImageUrl = mythicImageUrl;
		}

		public String Id { get; }

		public Int32 Order { get; }

		public String ImageUrl { get; }

		public String MythicImageUrl { get; }

		public async Task<Image> GetImageAsync()
		{
			return _image ?? (_image = await ImageClient.Instance.LoadImageAsync(this.ImageUrl));
		}

		public async Task<Image> GetMythicImageAsync()
		{
			return _mythicImage ?? (_mythicImage = await ImageClient.Instance.LoadImageAsync(this.MythicImageUrl));
		}

		public override String ToString()
		{
			return this.Id;
		}
	}
}
