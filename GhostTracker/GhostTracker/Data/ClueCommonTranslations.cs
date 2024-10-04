using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы ClueCommonTranslations.
    /// </summary>
    public class ClueCommonTranslations
    {
        [Key] public int Id { get; set; }
        public string LanguageCode { get; set; }
        public string OtherGhosts { get; set; }
        public string RelatedEquipment { get; set; }
    }
}