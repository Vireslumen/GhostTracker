using System.ComponentModel.DataAnnotations;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы GhostGuessQuestionTranslations, содержащей переводы на множество языков.
    /// </summary>
    public class GhostGuessQuestionTranslations
    {
        public GhostGuessQuestionBase GhostGuessQuestion { get; set; }
        public int GhostGuessQuestionBaseID { get; set; }
        [Key] public int ID { get; set; }
        public string LanguageCode { get; set; }
        public string QuestionText { get; set; }
    }
}