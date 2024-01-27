using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    /// Entity framework модель для таблицы QuestBase.
    /// </summary>
    public class QuestBase
    {
        [Key]
        public int ID { get; set; }

        public int Reward { get; set; }
        public ICollection<QuestTranslations> Translations { get; set; }
    }
}
