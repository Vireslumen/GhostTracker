using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    /// Entity framework модель для таблицы MapBase.
    /// </summary>
    public class MapBase
    {
        public int Exits { get; set; }

        public ObservableCollection<ExpandFieldWithImagesBase> ExpandFieldWithImagesBase { get; set; }

        public int Floors { get; set; }

        [Key]
        public int ID { get; set; }

        public string ImageFilePath { get; set; }
        public int RoomCount { get; set; }
        public ICollection<MapTranslations> Translations { get; set; }
        public ObservableCollection<UnfoldingItemBase> UnfoldingItemBase { get; set; }
        public int UnlockLevel { get; set; }
    }
}
