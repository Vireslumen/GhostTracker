using System.Collections.Generic;
using Newtonsoft.Json;

namespace GhostTracker.Models
{
    public class AppNewsRoot
    {
        [JsonProperty("appnews")] public AppNews AppNews { get; set; }
    }

    public class AppNews
    {
        [JsonProperty("newsitems")] public List<Patch> PatchItems { get; set; }
    }
}