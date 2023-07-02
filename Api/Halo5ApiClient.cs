using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace Halo5Reqs.Api
{
	public class Halo5ApiClient : ApiClientBase
	{
		public Halo5ApiClient(String token)
			: base("https://halo5-api-gateway.svc.halowaypoint.com", token, useCache: true)
		{
		}

		public Task<JObject> GetReqPackData(String id)
		{
			return this.HttpClient.GetJObjectAsync($"/en-us/req-packs/{id}");
		}

		public async Task<IEnumerable<Req>> GetReqsAsync()
		{
			JObject reqsData = await this.HttpClient.GetJObjectAsync("/en-us/reqs");
			return ((JArray)reqsData["reqs"])
				.Select(reqData => this.CreateReq((JObject)reqData));
		}

		private Req CreateReq(JObject reqData)
		{
			String categoryId = (String)reqData["category"];
			String subCategoryId = (String)reqData["subCategory"];
			return new Req(
				(String)reqData["id"],
				(String)reqData["name"],
				(String)reqData["description"],
				(Int32?)reqData["sellPrice"],
				(String)reqData["rarity"],
				(Boolean)reqData["mythic"],
				(Int32?)reqData["levelRequirement"],
				(String)reqData["certificationId"],
				(String)reqData["imageUrl"],
				(String)reqData["smallIconUrl"],
				(String)reqData["largeIconUrl"],
				categoryId,
				subCategoryId == "LoadoutWeapon"
					? (String)reqData["categoryDisplayName"]
					: subCategoryId,
				categoryId == "PowerAndVehicle"
					? (String)reqData["categoryDisplayName"]
					: null);
		}
	}
}
