using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы ImageWithDescriptionBase.
    /// </summary>
    public class ImageWithDescriptionBase
    {
        public ExpandFieldWithImagesBase ExpandFieldWithImagesBase { get; set; }
        public ICollection<ImageWithDescriptionTranslations> Translations { get; set; }
        public int ExpandFieldWithImagesBaseID { get; set; }
        [Key] public int ID { get; set; }
        public string ImageFilePath { get; set; }
    }
}