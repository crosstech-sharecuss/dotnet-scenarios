using System;

namespace LocalDistributedCache.Models
{
	public class CallbackData
	{
		public Guid UniqueIdentifier { get; set; }
		public string Title { get; set; }
		public string Body { get; set; }
	}
}
