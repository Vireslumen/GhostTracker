using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    ///     Представляет собой проклятый предмет, который расположен на карте.
    /// </summary>
    public class CursedPossession : BaseDisplayableItem
    {
        public int ID { get; set; }
        public List<ExpandFieldWithImages> ExpandFieldsWithImages { get; set; }
        public List<UnfoldingItem> UnfoldingItems { get; set; }
    }
}