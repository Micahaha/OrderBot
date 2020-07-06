using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace OrderBot
{
	public struct ConfigJson
	{
		[JsonProperty("token")]
		public string token { get; private set; }
		[JsonProperty("prefix")]
		public string Prefix { get; private set; }


		[JsonProperty("redditRefresh")]
		public string Refresh { get;  set; }
		[JsonProperty("redditAccess")]
		public string Access { get;  set; }
		[JsonProperty("redditAppId")]
		public string AppId { get;  set; }

	}
}
