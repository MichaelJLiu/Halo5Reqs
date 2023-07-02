using System;

namespace Halo5Reqs.Api
{
	public class Card
	{
		public Card(String reqId, Int32 unconsumed)
		{
			this.ReqId = reqId;
			this.Unconsumed = unconsumed;
		}

		public String ReqId { get; }

		public Int32 Unconsumed { get; set; }
	}
}
