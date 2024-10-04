using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GhostTracker.Models
{
    public class AppNewsRoot
    {
        [JsonProperty("appnews")]
        public AppNews AppNews { get; set; }
    }

    public class AppNews
    {
        [JsonProperty("newsitems")]
        public List<Patch> PatchItems { get; set; }
    }
}
