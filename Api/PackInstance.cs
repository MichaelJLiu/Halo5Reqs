using System;

namespace Halo5Reqs.Api
{
	public class PackInstance
	{
		public PackInstance(String id, DateTimeOffset acquiredDateTime, String giftSender, Boolean canBeOpened)
		{
			this.Id = id;
			this.AcquiredDateTime = acquiredDateTime;
			this.GiftSender = giftSender;
			this.CanBeOpened = canBeOpened;
		}

		public String Id { get; }

		public DateTimeOffset AcquiredDateTime { get; }

		public String GiftSender { get; }

		public Boolean CanBeOpened { get; set; }

		public CardInstance[] CardInstances { get; set; }

		public override String ToString()
		{
			return this.AcquiredDateTime.ToOffset(TimeSpan.Zero).AddHours(-15).ToString("d") +
				(this.GiftSender != null ? " from " + this.GiftSender : null);
		}
	}
}
