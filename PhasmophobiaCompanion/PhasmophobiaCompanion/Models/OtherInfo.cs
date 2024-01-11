using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PhasmophobiaCompanion.Models
{
    public class OtherInfo : ListItemBase
    {
        public ObservableCollection<ExpandFieldWithImages> ExpandFieldsWithImages { get; set; }
        public ObservableCollection<UnfoldingItem> UnfoldingItems { get; set; }
    }
}
