using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    /// Представляет собой проклятый предмет, который расположен на карте.
    /// </summary>
    public class CursedPossession : BaseDisplayableItem
    {
        public int ID { get; set; }
        public ObservableCollection<ExpandFieldWithImages> ExpandFieldsWithImages { get; set; }
        public ObservableCollection<UnfoldingItem> UnfoldingItems { get; set; }
    }
}
