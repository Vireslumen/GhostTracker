using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    /// Entity framework модель для таблицы QuestTranslations, содержащей переводы на множество языков.
    /// </summary>
    public class QuestTranslations
    {
        public string Clause { get; set; }

        public string Description { get; set; }

        [Key]
        public int ID { get; set; }

        public string LanguageCode { get; set; }
        public int QuestBaseID { get; set; }
    }
}
