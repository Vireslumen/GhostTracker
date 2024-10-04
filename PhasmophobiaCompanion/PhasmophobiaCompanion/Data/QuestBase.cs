using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы QuestBase.
    /// </summary>
    public class QuestBase
    {
        public ICollection<QuestTranslations> Translations { get; set; }
        [Key] public int ID { get; set; }
        public int Reward { get; set; }
    }
}