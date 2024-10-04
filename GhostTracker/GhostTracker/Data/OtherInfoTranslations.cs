using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы OtherInfoTranslations, содержащей переводы на множество языков.
    /// </summary>
    public class OtherInfoTranslations
    {
        [Key] public int Id { get; set; }
        public int OtherInfoBaseId { get; set; }
        public string Description { get; set; }
        public string LanguageCode { get; set; }
        public string Title { get; set; }
    }
}