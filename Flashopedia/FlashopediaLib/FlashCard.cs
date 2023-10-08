using Newtonsoft.Json;
using System;

namespace Flashopedia
{
	public class FlashCard
	{
		[JsonProperty("t")]
		public string Term { get; set; }
		[JsonProperty("d")]
		public string Definition { get; set; }
		[JsonProperty("r")]
		public bool IsMemorized { get; set; }
	}
}
