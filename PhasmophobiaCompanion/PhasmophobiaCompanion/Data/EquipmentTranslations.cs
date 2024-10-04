using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы EquipmentTranslations, содержащей переводы на множество языков.
    /// </summary>
    public class EquipmentTranslations
    {
        public int EquipmentBaseID { get; set; }
        [Key] public int ID { get; set; }
        public string Description { get; set; }
        public string LanguageCode { get; set; }
        public string Tier { get; set; }
        public string Title { get; set; }
    }
}