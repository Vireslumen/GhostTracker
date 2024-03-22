using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    ///     Представляет собой данные для некатегоризируемой страницы.
    /// </summary>
    public class OtherInfo : BaseDisplayableItem
    {
        public int ID { get; set; }
        public List<ExpandFieldWithImages> ExpandFieldsWithImages { get; set; }
        public List<UnfoldingItem> UnfoldingItems { get; set; }
    }
}