using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы GhostGuessQuestionTranslations, содержащей переводы на множество языков.
    /// </summary>
    public class GhostGuessQuestionTranslations
    {
        public GhostGuessQuestionBase GhostGuessQuestion { get; set; }
        public int GhostGuessQuestionBaseId { get; set; }
        [Key] public int Id { get; set; }
        public string LanguageCode { get; set; }
        public string QuestionText { get; set; }
    }
}