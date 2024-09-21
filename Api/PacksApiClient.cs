using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace Halo5Reqs.Api
{
	public class PacksApiClient : ApiClientBase
	{
		private readonly String _gamertag;
		private readonly Halo5ApiClient _halo5ApiClient;

		public PacksApiClient(String token, String gamertag, Halo5ApiClient halo5ApiClient)
			: base("https://packs.svc.halowaypoint.com", token)
		{
			_gamertag = gamertag;
			_halo5ApiClient = halo5ApiClient;
		}

		public async Task<IEnumerable<PackType>> GetPacksAsync()
		{
			JObject packTypesData = await this.HttpClient.GetJObjectAsync($"/h5/players/{Uri.EscapeDataString(_gamertag)}/packs?state=closed&start=0&count=100");
			List<Task<PackType>> packTypeTasks = ((JArray)packTypesData["Results"])
				.Select(packTypeData => this.CreatePackTypeAsync((JObject)packTypeData))
				.ToList();
			return await Task.WhenAll(packTypeTasks);
		}

		private async Task<PackType> CreatePackTypeAsync(JObject packTypeData)
		{
			String id = (String)packTypeData["Id"];
			JObject staticPackTypeData = await _halo5ApiClient.GetReqPackData(id);
			return new PackType(
				id,
				(String)staticPackTypeData["name"],
				(String)staticPackTypeData["description"],
				(String)staticPackTypeData["iconUrl"],
				null,
				(String)staticPackTypeData["backgroundImageUrl"],
				(String)staticPackTypeData["flairImageUrl"],
				(String)staticPackTypeData["giftBannerUrl"],
				(String)staticPackTypeData["giftIconUrl"],
				((JArray)packTypeData["Result"]["Instances"])
					.Select(packInstanceData => CreatePackInstance((JObject)packInstanceData))
					.ToList());
		}

		private PackInstance CreatePackInstance(JObject packInstanceData)
		{
			return new PackInstance(
				(String)packInstanceData["Id"],
				(DateTimeOffset)packInstanceData["AcquiredDateUtc"]["ISO8601Date"],
				(String)packInstanceData.SelectToken("GiftSender.Gamertag"),
				(Boolean)packInstanceData["CanBeOpened"]);
		}

		public async Task<IEnumerable<PackType>> GetStorePacksAsync()
		{
			JObject storeData = await this.HttpClient.GetJObjectAsync($"/h5/players/{Uri.EscapeDataString(_gamertag)}/store");
			List<Task<PackType>> packTypeTasks = storeData["Offers"]
				.Where(packTypeData => (Int32)packTypeData["OfferType"] == 1)
				.Select(packTypeData => this.CreateStorePackType((JObject)packTypeData))
				.ToList();
			return (await Task.WhenAll(packTypeTasks))
				.Where(packType => packType.CreditPrice != null);
		}

		private async Task<PackType> CreateStorePackType(JObject packTypeData)
		{
			String id = (String)packTypeData["Id"];
			JObject staticPackTypeData = await _halo5ApiClient.GetReqPackData(id);
			return new PackType(
				id,
				(String)staticPackTypeData["name"],
				(String)staticPackTypeData["description"],
				(String)staticPackTypeData["iconUrl"],
				(Int32?)staticPackTypeData["creditPrice"],
				(String)staticPackTypeData["backgroundImageUrl"],
				(String)staticPackTypeData["flairImageUrl"],
				(String)staticPackTypeData["giftBannerUrl"],
				(String)staticPackTypeData["giftIconUrl"],
				null);
		}

		public async Task<IEnumerable<Card>> GetCardsAsync() // TODO: Switch to IAsyncEnumerable
		{
			List<Card> cards = new List<Card>();
			JObject cardsData;

			do
			{
				cardsData = await this.HttpClient.GetJObjectAsync($"/h5/players/{Uri.EscapeDataString(_gamertag)}/cards?state=unconsumed&start={cards.Count}&count=100");
				cards.AddRange(((JArray)cardsData["Results"])
					.Select(cardData => this.CreateCard((JObject)cardData)));
			}
			while (cards.Count < (Int32)cardsData["TotalCount"]);

			return cards;
		}

		private Card CreateCard(JObject cardData)
		{
			return new Card(
				((String)cardData["Id"]).Replace("-", ""),
				(Int32)cardData["Result"]["TotalByState"]["Unconsumed"]);
		}

		public async Task OpenPackAsync(PackType packType, PackInstance packInstance)
		{
			JObject payload = new JObject(
				new JProperty("State", "Opened"),
				new JProperty("RequestorDetail", "Spartan Token"),
				new JProperty("Reason", "Selling via waypoint"),
				new JProperty("TrackingId", Guid.NewGuid().ToString()));
			JObject packInstanceData = await this.HttpClient.PatchJObjectAsync($"/h5/players/{Uri.EscapeDataString(_gamertag)}/packs/{packType.Id}/{packInstance.Id}", payload);
			packInstance.CardInstances = packInstanceData["CardInstances"]
				.Select(cardInstanceData => this.CreateCardInstance((JObject)cardInstanceData))
				.ToArray();
		}

		private CardInstance CreateCardInstance(JObject cardInstanceData)
		{
			return new CardInstance(
				(String)cardInstanceData["Id"],
				((String)cardInstanceData["TypeId"]).Replace("-", ""),
				cardInstanceData["ConsumedDateUtc"].Type != JTokenType.Null);
		}

		public async Task<String> BuyPackAsync(PackType packType)
		{
			JObject payload = new JObject(
				new JProperty("RequestorDetail", "Spartan Token"),
				new JProperty("Reason", "Buying via waypoint"),
				new JProperty("TrackingId", Guid.NewGuid().ToString()),
				new JProperty("AcquisitionMethod", "Credits"),
				new JProperty("ExpectedPrice", packType.CreditPrice));
			JObject packInstanceData = await this.HttpClient.PostJObjectAsync($"/h5/players/{Uri.EscapeDataString(_gamertag)}/packs/{packType.Id}", payload);
			return (String)packInstanceData["Id"];
		}

		public async Task<String> SellCardAsync(Req req, Card card, String cardInstanceId, Int32 minReqCount)
		{
			JObject cardInstancesData = await this.HttpClient.GetJObjectAsync($"/h5/players/{Uri.EscapeDataString(_gamertag)}/cards/{req.Id}?state=unconsumed&start=0&count={minReqCount + 1}");
			Int32 resultCount = (Int32)cardInstancesData["ResultCount"];

			if (resultCount > minReqCount)
			{
				if (cardInstanceId == null)
				{
					cardInstanceId = (String)cardInstancesData["Results"][0]["Id"];
				}

				JObject payload = new JObject(
					new JProperty("ExpectedPrice", req.SellPrice.Value),
					new JProperty("State", "Sold"),
					new JProperty("RequestorDetail", "Spartan Token"),
					new JProperty("Reason", "Selling via waypoint"),
					new JProperty("TrackingId", Guid.NewGuid().ToString()));
				await this.HttpClient.PatchJObjectAsync($"/h5/players/{Uri.EscapeDataString(_gamertag)}/cards/{req.Id}/{cardInstanceId}", payload);
				card.Unconsumed--;
				return cardInstanceId;
			}
			else
			{
				card.Unconsumed = resultCount;
				return null;
			}
		}
	}
}
