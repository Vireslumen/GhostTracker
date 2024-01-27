using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    /// Entity framework модель для таблицы CursedPossessionBase.
    /// </summary>
    public class CursedPossessionBase
    {
        public ObservableCollection<ExpandFieldWithImagesBase> ExpandFieldWithImagesBase { get; set; }

        [Key]
        public int ID { get; set; }

        public string ImageFilePath { get; set; }
        public ICollection<CursedPossessionTranslations> Translations { get; set; }
        public ObservableCollection<UnfoldingItemBase> UnfoldingItemBase { get; set; }
    }
}
