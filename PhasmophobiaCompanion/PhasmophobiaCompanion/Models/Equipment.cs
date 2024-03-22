using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    ///     Представляет собой снаряжение.
    /// </summary>
    public class Equipment : BaseDisplayableItem
    {
        public int Cost { get; set; }
        public int ID { get; set; }
        public int MaxLimit { get; set; }
        public int UnlockCost { get; set; }
        public int UnlockLevel { get; set; }
        /// <summary>
        ///     Прочие характеристики снаряжения.
        /// </summary>
        public List<OtherEquipmentStat> OtherEquipmentStats { get; set; }
        public List<UnfoldingItem> UnfoldingItems { get; set; }
        public string Tier { get; set; }
    }
}