using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы MapBase.
    /// </summary>
    public class MapBase
    {
        public ICollection<MapTranslations> Translations { get; set; }
        public int Exits { get; set; }
        public int Floors { get; set; }
        [Key] public int ID { get; set; }
        public int RoomCount { get; set; }
        public int UnlockLevel { get; set; }
        public ObservableCollection<ExpandFieldWithImagesBase> ExpandFieldWithImagesBase { get; set; }
        public ObservableCollection<UnfoldingItemBase> UnfoldingItemBase { get; set; }
        public string ImageFilePath { get; set; }
    }
}