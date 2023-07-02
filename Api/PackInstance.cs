using System;

namespace Halo5Reqs.Api
{
	public class PackInstance
	{
		public PackInstance(String id, DateTime acquiredDateTime, String giftSender, Boolean canBeOpened)
		{
			this.Id = id;
			this.AcquiredDateTime = acquiredDateTime;
			this.GiftSender = giftSender;
			this.CanBeOpened = canBeOpened;
		}

		public String Id { get; }

		public DateTime AcquiredDateTime { get; }

		public String GiftSender { get; }

		public Boolean CanBeOpened { get; set; }

		public CardInstance[] CardInstances { get; set; }

		public override String ToString()
		{
			return this.AcquiredDateTime.ToLocalTime().ToShortDateString() +
				(this.GiftSender != null ? " from " + this.GiftSender : null);
		}
	}
}
