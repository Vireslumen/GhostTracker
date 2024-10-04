using System.Collections.Generic;

namespace GhostTracker.Models
{
    /// <summary>
    ///     Представляет собой снаряжение.
    /// </summary>
    public class Equipment : BaseTitledItem
    {
        public int Cost { get; set; }
        public int Id { get; set; }
        public int MaxLimit { get; set; }
        public int UnlockCost { get; set; }
        public int UnlockLevel { get; set; }
        public List<Clue> EquipmentRelatedClues { get; set; }
        public List<int> CluesId { get; set; }
        /// <summary>
        ///     Прочие характеристики снаряжения.
        /// </summary>
        public List<OtherEquipmentStat> OtherEquipmentStats { get; set; }
        public List<UnfoldingItem> UnfoldingItems { get; set; }
        public string Tier { get; set; }
    }
}