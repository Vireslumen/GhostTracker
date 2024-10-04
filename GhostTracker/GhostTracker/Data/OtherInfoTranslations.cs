using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы OtherInfoTranslations, содержащей переводы на множество языков.
    /// </summary>
    public class OtherInfoTranslations
    {
        [Key] public int ID { get; set; }
        public int OtherInfoBaseID { get; set; }
        public string Description { get; set; }
        public string LanguageCode { get; set; }
        public string Title { get; set; }
    }
}