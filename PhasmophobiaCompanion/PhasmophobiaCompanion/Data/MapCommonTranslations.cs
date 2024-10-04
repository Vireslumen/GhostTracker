using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы MapCommonTranslations.
    /// </summary>
    public class MapCommonTranslations
    {
        [Key] public int ID { get; set; }
        public string Apply { get; set; }
        public string Clear { get; set; }
        public string EmptyView { get; set; }
        public string Exits { get; set; }
        public string FilterRoom { get; set; }
        public string FilterSize { get; set; }
        public string Floors { get; set; }
        public string HidenSpot { get; set; }
        public string LanguageCode { get; set; }
        public string MapSize { get; set; }
        public string MapsTitle { get; set; }
        public string RoomNumber { get; set; }
        public string Search { get; set; }
        public string UnlockLvl { get; set; }
    }
}