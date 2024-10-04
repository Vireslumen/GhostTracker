using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы OtherInfoBase.
    /// </summary>
    public class OtherInfoBase
    {
        public ICollection<OtherInfoTranslations> Translations { get; set; }
        [Key] public int ID { get; set; }
        public List<ExpandFieldWithImagesBase> ExpandFieldWithImagesBase { get; set; }
        public List<UnfoldingItemBase> UnfoldingItemBase { get; set; }
        public string ImageFilePath { get; set; }
    }
}