using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы ChallengeModeBase.
    /// </summary>
    public class ChallengeModeBase
    {
        public ICollection<ChallengeModeTranslations> Translations { get; set; }
        public int DifficultyID { get; set; }
        [Key] public int ID { get; set; }
        public int MapID { get; set; }
        public List<EquipmentBase> EquipmentBase { get; set; }
    }
}