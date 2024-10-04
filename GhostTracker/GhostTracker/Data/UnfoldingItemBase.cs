using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы UnfoldingItemBase.
    /// </summary>
    public class UnfoldingItemBase
    {
        public ICollection<UnfoldingItemTranslations> Translations { get; set; }
        [Key] public int Id { get; set; }
        public List<ClueBase> ClueBase { get; set; }
        public List<CursedPossessionBase> CursedPossessionBase { get; set; }
        public List<EquipmentBase> EquipmentBase { get; set; }
        public List<GhostBase> GhostBase { get; set; }
        public List<MapBase> MapBase { get; set; }
        public List<OtherInfoBase> OtherInfoBase { get; set; }
    }
}