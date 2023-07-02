using System;

namespace Halo5Reqs.Api
{
	public class CardInstance
	{
		public CardInstance(String id, String reqId, Boolean consumed)
		{
			this.Id = id;
			this.ReqId = reqId;
			this.Consumed = consumed;
		}

		public String Id { get; }

		public String ReqId { get; }

		public Boolean Consumed { get; set; }
	}
}
