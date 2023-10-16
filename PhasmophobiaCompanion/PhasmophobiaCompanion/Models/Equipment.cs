using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PhasmophobiaCompanion.Models
{
    public class Equipment : ListItemBase
    {
        public int Level { get; set; }
        public string Tier { get; set; }
        public int Cost { get; set; }
        public int UnlockCost { get; set; }
        public int MaxLimit { get; set; }
        public string Uses { get; set; }
        public ObservableCollection<string> OtherStats { get; set; }
        public ObservableCollection<UnfoldingItem> UnfoldingItems { get; set; }
    }
}
