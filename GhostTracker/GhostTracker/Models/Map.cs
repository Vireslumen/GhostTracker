using System.Collections.Generic;

namespace GhostTracker.Models
{
    /// <summary>
    ///     Представляет собой карту для игровой сессии.
    /// </summary>
    public class Map : BaseTitledItem
    {
        /// <summary>
        ///     Количество выходов.
        /// </summary>
        public int Exits { get; set; }
        /// <summary>
        ///     Количество этажей.
        /// </summary>
        public int Floors { get; set; }
        public int ID { get; set; }
        /// <summary>
        ///     Количество комнат.
        /// </summary>
        public int RoomCount { get; set; }
        /// <summary>
        ///     Размер карты в цифровом значении
        /// </summary>
        public int SizeNumeric { get; set; }
        public int UnlockLevel { get; set; }
        public List<ExpandFieldWithImages> ExpandFieldsWithImages { get; set; }
        public List<UnfoldingItem> UnfoldingItems { get; set; }
        /// <summary>
        ///     Количество мест для прятания.
        /// </summary>
        public string HidingSpotCount { get; set; }
        /// <summary>
        ///     Размер карты.
        /// </summary>
        public string Size { get; set; }
    }
}