using System;
using System.Drawing;
using System.Threading.Tasks;

namespace Halo5Reqs.Api
{
	public class ReqSubCategory
	{
		private Image _icon;

		public ReqSubCategory(String id, String iconUrl, String name = null)
		{
			this.Id = id;
			this.Name = name ?? id;
			this.IconUrl = iconUrl;
		}

		public String Id { get; }

		public String Name { get; }

		public String IconUrl { get; }

		public async Task<Image> GetIconAsync()
		{
			return _icon ?? (this.IconUrl != null ? _icon = await ImageClient.Instance.LoadImageAsync(this.IconUrl) : null);
		}

		public override String ToString()
		{
			return this.Name;
		}
	}
}
