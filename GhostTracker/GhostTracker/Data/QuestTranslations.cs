using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы QuestTranslations, содержащей переводы на множество языков.
    /// </summary>
    public class QuestTranslations
    {
        [Key] public int Id { get; set; }
        public string Clause { get; set; }
        public string Title { get; set; }
        public string LanguageCode { get; set; }
        public string Tip { get; set; }
        public string Type { get; set; }
    }
}