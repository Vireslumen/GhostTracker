using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы FeedbackCommonTranslations.
    /// </summary>
    public class FeedbackCommonTranslations
    {
        [Key] public int Id { get; set; }
        public string Cancel { get; set; }
        public string EditorTip { get; set; }
        public string LanguageCode { get; set; }
        public string Submit { get; set; }
        public string Title { get; set; }
    }
}