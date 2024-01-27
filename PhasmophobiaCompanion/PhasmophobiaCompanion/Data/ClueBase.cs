using PhasmophobiaCompanion.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    /// Entity framework модель для таблицы ClueBase.
    /// </summary>
    public class ClueBase
    {
        public string IconFilePath { get; set; }

        public ObservableCollection<ExpandFieldWithImagesBase> ExpandFieldWithImagesBase { get; set; }

        public ObservableCollection<GhostBase> GhostBase { get; set; }

        [Key]
        public int ID { get; set; }

        public string ImageFilePath { get; set; }
        public ICollection<ClueTranslations> Translations { get; set; }
        public ObservableCollection<UnfoldingItemBase> UnfoldingItemBase { get; set; }
    }
}
