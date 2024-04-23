using System.ComponentModel.DataAnnotations;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы OtherEquipmentStatBase, содержащей переводы на множество языков.
    /// </summary>
    public class OtherEquipmentStatBase
    {
        public int EquipmentBaseID { get; set; }
        [Key] public int ID { get; set; }
        public string LanguageCode { get; set; }
        public string Stat { get; set; }
        public string ShortStat { get; set; }
    }
}