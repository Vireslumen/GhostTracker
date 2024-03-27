using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы DifficultyBase.
    /// </summary>
    public class DifficultyBase
    {
        public float RewardMultiplier { get; set; }
        public float SanityConsumption { get; set; }
        public ICollection<DifficultyTranslations> Translations { get; set; }
        public int EvidenceAvailable { get; set; }
        [Key] public int ID { get; set; }
        public int SanityRestoration { get; set; }
        public int SetupTime { get; set; }
        public int UnlockLevel { get; set; }
    }
}