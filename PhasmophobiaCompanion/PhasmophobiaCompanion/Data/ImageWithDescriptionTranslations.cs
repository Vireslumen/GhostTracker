using System.ComponentModel.DataAnnotations;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы ImageWithDescriptionTranslations, содержащей переводы на множество языков.
    /// </summary>
    public class ImageWithDescriptionTranslations
    {
        public ImageWithDescriptionBase ImageWithDescription { get; set; }
        [Key] public int ID { get; set; }
        public int ImageWithDescriptionBaseID { get; set; }
        public string Description { get; set; }
        public string LanguageCode { get; set; }
    }
}