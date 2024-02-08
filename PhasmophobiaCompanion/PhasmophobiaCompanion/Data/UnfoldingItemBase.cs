using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы UnfoldingItemBase.
    /// </summary>
    public class UnfoldingItemBase
    {
        public ICollection<UnfoldingItemTranslations> Translations { get; set; }
        [Key] public int ID { get; set; }
        public ObservableCollection<ClueBase> ClueBase { get; set; }
        public ObservableCollection<CursedPossessionBase> CursedPossessionBase { get; set; }
        public ObservableCollection<EquipmentBase> EquipmentBase { get; set; }
        public ObservableCollection<GhostBase> GhostBase { get; set; }
        public ObservableCollection<MapBase> MapBase { get; set; }
        public ObservableCollection<OtherInfoBase> OtherInfoBase { get; set; }
    }
}