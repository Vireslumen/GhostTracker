using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы ImageWithDescriptionTranslations, содержащей переводы на множество языков.
    /// </summary>
    public class ImageWithDescriptionTranslations
    {
        public ImageWithDescriptionBase ImageWithDescription { get; set; }
        [Key] public int Id { get; set; }
        public int ImageWithDescriptionBaseId { get; set; }
        public string Description { get; set; }
        public string LanguageCode { get; set; }
    }
}