using System.Collections.Generic;

namespace GhostTracker.Models
{
    /// <summary>
    ///     Представляет собой снаряжение.
    /// </summary>
    public class Equipment : BaseTitledItem
    {
        /// <summary>
        ///     Цена снаряжения.
        /// </summary>
        public int Cost { get; set; }
        public int Id { get; set; }
        /// <summary>
        ///     Лимит снаряжения в одном матче.
        /// </summary>
        public int MaxLimit { get; set; }
        /// <summary>
        ///     Цена разблокировки снаряжения.
        /// </summary>
        public int UnlockCost { get; set; }
        /// <summary>
        ///     Уровень разблокировки снаряжения.
        /// </summary>
        public int UnlockLevel { get; set; }
        /// <summary>
        ///     Список доказательств связанных с этим снаряжением.
        /// </summary>
        public List<Clue> EquipmentRelatedClues { get; set; }
        public List<int> CluesId { get; set; }
        /// <summary>
        ///     Прочие характеристики снаряжения.
        /// </summary>
        public List<OtherEquipmentStat> OtherEquipmentStats { get; set; }
        public List<UnfoldingItem> UnfoldingItems { get; set; }
        /// <summary>
        ///     Класс снаряжения.
        /// </summary>
        public string Tier { get; set; }
    }
}