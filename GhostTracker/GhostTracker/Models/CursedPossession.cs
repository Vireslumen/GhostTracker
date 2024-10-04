using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GhostTracker.Models
{
    /// <summary>
    ///     Представляет собой проклятый предмет, который расположен на карте.
    /// </summary>
    public class CursedPossession : BaseTitledItem
    {
        public int ID { get; set; }
        public List<ExpandFieldWithImages> ExpandFieldsWithImages { get; set; }
        public List<UnfoldingItem> UnfoldingItems { get; set; }
    }
}