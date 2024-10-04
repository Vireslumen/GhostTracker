using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы MapBase.
    /// </summary>
    public class MapBase
    {
        public ICollection<MapTranslations> Translations { get; set; }
        public int Exits { get; set; }
        public int Floors { get; set; }
        [Key] public int Id { get; set; }
        public int RoomCount { get; set; }
        public int SizeNumeric { get; set; }
        public int UnlockLevel { get; set; }
        public List<ExpandFieldWithImagesBase> ExpandFieldWithImagesBase { get; set; }
        public List<UnfoldingItemBase> UnfoldingItemBase { get; set; }
        public string ImageFilePath { get; set; }
    }
}