using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PhasmophobiaCompanion.Models
{
    public class Ghost : ListItemBase
    {
        public ObservableCollection<CluesStructure> Clues { get; set; }
        public ObservableCollection<UnfoldingItem> UnfoldingItems { get; set; }
        public string Identification { get; set; }

    }
}