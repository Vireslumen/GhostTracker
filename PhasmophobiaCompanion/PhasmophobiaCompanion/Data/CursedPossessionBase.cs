using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы CursedPossessionBase.
    /// </summary>
    public class CursedPossessionBase
    {
        public ICollection<CursedPossessionTranslations> Translations { get; set; }
        [Key] public int ID { get; set; }
        public List<ExpandFieldWithImagesBase> ExpandFieldWithImagesBase { get; set; }
        public List<UnfoldingItemBase> UnfoldingItemBase { get; set; }
        public string ImageFilePath { get; set; }
    }
}