using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы MapTranslations, содержащей переводы на множество языков.
    /// </summary>
    public class MapTranslations
    {
        [Key] public int Id { get; set; }
        public string Description { get; set; }
        public string HidingSpotCount { get; set; }
        public string LanguageCode { get; set; }
        public string Size { get; set; }
        public string Title { get; set; }
    }
}