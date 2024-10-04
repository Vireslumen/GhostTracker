using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы ImageWithDescriptionBase.
    /// </summary>
    public class ImageWithDescriptionBase
    {
        public ExpandFieldWithImagesBase ExpandFieldWithImagesBase { get; set; }
        public ICollection<ImageWithDescriptionTranslations> Translations { get; set; }
        public int ExpandFieldWithImagesBaseId { get; set; }
        [Key] public int Id { get; set; }
        public string ImageFilePath { get; set; }
    }
}