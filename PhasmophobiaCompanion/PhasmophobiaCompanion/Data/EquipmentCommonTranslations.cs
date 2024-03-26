using System.ComponentModel.DataAnnotations;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы EquipmentCommonTranslations.
    /// </summary>
    public class EquipmentCommonTranslations
    {
        [Key] public int ID { get; set; }
        public string Apply { get; set; }
        public string Clear { get; set; }
        public string EmptyView { get; set; }
        public string EquipmentsTitle { get; set; }
        public string FilterTier { get; set; }
        public string FilterUnlock { get; set; }
        public string LanguageCode { get; set; }
        public string MaxLimit { get; set; }
        public string OtherTier { get; set; }
        public string Price { get; set; }
        public string PriceUnlock { get; set; }
        public string Search { get; set; }
        public string Tier { get; set; }
        public string UnlockLevel { get; set; }
    }
}