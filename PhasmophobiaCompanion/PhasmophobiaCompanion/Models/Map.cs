using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PhasmophobiaCompanion.Models
{
    public class Map : ListItemBase
    {
        public string Size { get; set; }
        public int RoomCount { get; set; }
        public int UnlockLvl { get; set; }
        public int Exits { get; set; }
        public int Floors { get; set; }
        public string HidenSpotCount { get; set; }
        public ExpandFieldWithImages Structure { get; set; }
        public ExpandFieldWithImages CursedLocations { get; set; }
        public ExpandFieldWithImages HidingSpotLocation { get; set; }
        public ExpandFieldWithImages CyclingGhostSpots { get; set; }
        public ObservableCollection<UnfoldingItem> UnfoldingItems { get; set; }

    }
}
