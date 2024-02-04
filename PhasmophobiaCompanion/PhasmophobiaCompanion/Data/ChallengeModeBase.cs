using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    /// Entity framework модель для таблицы ChallengeModeBase.
    /// </summary>
    public class ChallengeModeBase
    {
        [Key]
        public int ID { get; set; }
        public int MapID { get; set; }
        public int DifficultyID { get; set; }
        public ICollection<ChallengeModeTranslations> Translations { get; set; }
        public List<EquipmentBase> EquipmentBase { get; set; }
    }
}
