using System;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace Halo5Reqs.Api
{
	public class ProfileApiClient : ApiClientBase
	{
		public ProfileApiClient(String token)
			: base("https://profile.svc.halowaypoint.com", token)
		{
		}

		public async Task<String> GetGamertag()
		{
			JObject userData = await this.HttpClient.GetJObjectAsync("/users/me");
			return (String)userData["gamertag"];
		}
	}
}
