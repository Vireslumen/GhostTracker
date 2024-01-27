using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    /// Entity framework модель для таблицы ImageWithDescriptionTranslations, содержащей переводы на множество языков.
    /// </summary>
    public class ImageWithDescriptionTranslations
    {
        public string Description { get; set; }

        [Key]
        public int ID { get; set; }

        public ImageWithDescriptionBase ImageWithDescription { get; set; }
        public int ImageWithDescriptionBaseID { get; set; }
        public string LanguageCode { get; set; }
    }
}
