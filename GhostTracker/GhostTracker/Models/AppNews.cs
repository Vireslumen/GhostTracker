using System.Collections.Generic;
using Newtonsoft.Json;

namespace GhostTracker.Models
{
    /// <summary>
    ///     Представляет корневой объект для данных новостей приложения, загруженных через API Steam.
    /// </summary>
    public class AppNewsRoot
    {
        /// <summary>
        ///     Содержит объект новостей приложения - AppNews.
        /// </summary>
        [JsonProperty("appnews")]
        public AppNews AppNews { get; set; }
    }

    /// <summary>
    ///     Представляет объект новостей приложения, содержащий список патчей.
    /// </summary>
    public class AppNews
    {
        /// <summary>
        ///     Список элементов новостей, связанных с патчами - Patch.
        /// </summary>
        [JsonProperty("newsitems")]
        public List<Patch> PatchItems { get; set; }
    }
}