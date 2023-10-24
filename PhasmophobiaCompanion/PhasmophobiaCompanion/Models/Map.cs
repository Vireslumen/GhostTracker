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
        public ObservableCollection<ExpandFieldWithImages> ExpandFieldsWithImages { get; set; }
        public ObservableCollection<UnfoldingItem> UnfoldingItems { get; set; }

    }
}
