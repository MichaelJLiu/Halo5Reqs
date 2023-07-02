using System;
using System.Collections.Generic;
using System.Linq;

namespace Halo5Reqs.Api
{
	public class ReqCategory
	{
		private const String LoadoutWeaponIconUrl = "https://content.halocdn.com/media/Default/games/halo-5-guardians/reqs/req-category-icons/category_icon_07-804d5df47967476ab79e5f02f7880c36.png";

		private static readonly ReqCategory[] s_categories =
			{
				new ReqCategory("Customization",
					new ReqSubCategory("Announcer", "https://content.halocdn.com/media/Default/games/halo-5-guardians/reqs/req-category-icons/category_icon_18-d3f98c9c20064ed9a0fac3de11510a08.png"),
					new ReqSubCategory("Helmet", "https://content.halocdn.com/media/Default/games/halo-5-guardians/reqs/req-category-icons/category_icon_01-3a52c28429124e3a8b875731bcafa4a3.png"),
					new ReqSubCategory("Armor", "https://content.halocdn.com/media/Default/games/halo-5-guardians/reqs/req-category-icons/category_icon_00-03721e9e2fe1490b96f52d17a3a14ac4.png"),
					new ReqSubCategory("Visor", "https://content.halocdn.com/media/Default/games/halo-5-guardians/reqs/req-category-icons/category_icon_02-1f4ad8818bdd489e83f015a2bcc5f34a.png"),
					new ReqSubCategory("Emblem", "https://content.halocdn.com/media/Default/games/halo-5-guardians/reqs/req-category-icons/category_icon_03-478f85d278b34a9f84b5b8af6f3af60a.png"),
					new ReqSubCategory("Stance", "https://content.halocdn.com/media/Default/games/halo-5-guardians/reqs/req-category-icons/category_icon_04-1b2156508a80458091c9300a73860284.png"),
					new ReqSubCategory("Assassination", "https://content.halocdn.com/media/Default/games/halo-5-guardians/reqs/req-category-icons/category_icon_05-791bf2697761401d85d528ff37e85499.png"),
					new ReqSubCategory("WeaponSkin", "https://content.halocdn.com/media/Default/games/halo-5-guardians/reqs/req-category-icons/category_icon_16-a994bbec4b0044e2bca45ea5d4af6652.png", "Weapon Skin")),
				new ReqCategory("Loadout",
					new ReqSubCategory("Assault Rifle", LoadoutWeaponIconUrl),
					new ReqSubCategory("Battle Rifle", LoadoutWeaponIconUrl),
					new ReqSubCategory("DMR", LoadoutWeaponIconUrl),
					new ReqSubCategory("Halo 2 Battle Rifle", LoadoutWeaponIconUrl),
					new ReqSubCategory("MAGNUM", LoadoutWeaponIconUrl, "Magnum"),
					new ReqSubCategory("SMG", LoadoutWeaponIconUrl),
					new ReqSubCategory("ArmorMod", "https://content.halocdn.com/media/Default/games/halo-5-guardians/reqs/req-category-icons/category_icon_11-7a5db4b7dbb14107a0302b35be6e7fb9.png", "Armor Mod")),
				new ReqCategory("PowerAndVehicle", "Power & Vehicle",
					new ReqSubCategory("PowerWeapon", "https://content.halocdn.com/media/Default/games/halo-5-guardians/reqs/req-category-icons/category_icon_08-224e1bd17591426c8cbbc254f4a29720.png", "Power Weapon"),
					new ReqSubCategory("Vehicle", "https://content.halocdn.com/media/Default/games/halo-5-guardians/reqs/req-category-icons/category_icon_09-6b1e3b0093b64685bb0c09a33e24a7d3.png"),
					new ReqSubCategory("Equipment", "https://content.halocdn.com/media/Default/games/halo-5-guardians/reqs/req-category-icons/category_icon_12-6c279cbfcf3a402aba819c1c0629d0fa.png")),
				new ReqCategory("Boost",
					new ReqSubCategory("Arena", "https://content.halocdn.com/media/Default/games/halo-5-guardians/reqs/req-category-icons/category_icon_13-dc836b8f7162484b924e30b0ff7e27b2.png"),
					new ReqSubCategory("Warzone", "https://content.halocdn.com/media/Default/games/halo-5-guardians/reqs/req-category-icons/category_icon_13-dc836b8f7162484b924e30b0ff7e27b2.png")),
				new ReqCategory("Other",
					new ReqSubCategory("Other", null)),
			};

		private static readonly Dictionary<String, ReqCategory> s_categoriesById =
			s_categories.ToDictionary(category => category.Id);

		public static IEnumerable<ReqCategory> AllCategories => s_categories;

		public static ReqCategory FindById(String id)
		{
			return s_categoriesById[id];
		}

		private readonly Dictionary<String, ReqSubCategory> _subCategoriesById;

		public ReqCategory(String id, params ReqSubCategory[] subCategories)
			: this(id, id, subCategories)
		{
		}

		public ReqCategory(String id, String name, params ReqSubCategory[] subCategories)
		{
			this.Id = id;
			this.Name = name ?? id;
			_subCategoriesById = subCategories.ToDictionary(subCategory => subCategory.Id);
		}

		public String Id { get; }

		public String Name { get; }

		public IEnumerable<ReqSubCategory> SubCategories => _subCategoriesById.Values;

		public ReqSubCategory FindSubCategoryById(String id)
		{
			return _subCategoriesById[id];
		}

		public override String ToString()
		{
			return this.Name;
		}
	}
}
