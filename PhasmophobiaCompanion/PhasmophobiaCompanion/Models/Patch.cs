using Newtonsoft.Json;

namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    ///     Представляет собой информацию о патче, с сылкой на его страницу.
    /// </summary>
    public class Patch
    {
        public int ID { get; set; }
        /// <summary>
        ///     Ссылка на страницу описания патча.
        /// </summary>

        [JsonProperty("url")]
        public string Source { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}