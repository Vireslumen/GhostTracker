using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GhostTracker.Models
{
    /// <summary>
    ///     Представляет собой данные для некатегоризируемой страницы.
    /// </summary>
    public class OtherInfo : BaseTitledItem
    {
        public int ID { get; set; }
        public List<ExpandFieldWithImages> ExpandFieldsWithImages { get; set; }
        public List<UnfoldingItem> UnfoldingItems { get; set; }
    }
}