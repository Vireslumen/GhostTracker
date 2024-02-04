using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    /// Представляет собой карту для игровой сессии.
    /// </summary>
    public class Map : BaseDisplayableItem
    {
        public int ID { get; set; }
        // Количество выходов.
        public int Exits { get; set; }
        public ObservableCollection<ExpandFieldWithImages> ExpandFieldsWithImages { get; set; }

        // Количество этажей.
        public int Floors { get; set; }

        // Количество мест для прятания.
        public string HidingSpotCount { get; set; }

        // Количество комнат.
        public int RoomCount { get; set; }

        // Размер карты.
        public string Size { get; set; }
        public ObservableCollection<UnfoldingItem> UnfoldingItems { get; set; }

        // Уровень необходимый для разблокировки карты.
        public int UnlockLevel { get; set; }
    }
}
