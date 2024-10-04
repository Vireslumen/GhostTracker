using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы GhostGuessQuestionBase.
    /// </summary>
    public class GhostGuessQuestionBase
    {
        public ICollection<GhostGuessQuestionTranslations> Translations { get; set; }
        public int AnswerMeaning { get; set; }
        public int AnswerNegativeMeaning { get; set; }
        [Key] public int ID { get; set; }
        public List<GhostBase> GhostBase { get; set; }
    }
}