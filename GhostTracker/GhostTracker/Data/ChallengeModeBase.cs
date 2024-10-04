using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы ChallengeModeBase.
    /// </summary>
    public class ChallengeModeBase
    {
        public ICollection<ChallengeModeTranslations> Translations { get; set; }
        [Key] public int Id { get; set; }
        public int MapId { get; set; }
        public List<EquipmentBase> EquipmentBase { get; set; }
    }
}