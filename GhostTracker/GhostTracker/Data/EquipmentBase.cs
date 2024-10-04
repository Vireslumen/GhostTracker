using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы EquipmentBase.
    /// </summary>
    public class EquipmentBase
    {
        public ICollection<ChallengeModeBase> ChallengeModeBase { get; set; }
        public ICollection<ClueBase> ClueBase { get; set; }
        public ICollection<EquipmentTranslations> Translations { get; set; }
        public int Cost { get; set; }
        [Key] public int Id { get; set; }
        public int MaxLimit { get; set; }
        public int UnlockCost { get; set; }
        public int UnlockLevel { get; set; }
        public List<OtherEquipmentStatBase> OtherEquipmentStatBase { get; set; }
        public List<UnfoldingItemBase> UnfoldingItemBase { get; set; }
        public string ImageFilePath { get; set; }
    }
}