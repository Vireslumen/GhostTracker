using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    /// Entity framework модель для таблицы ClueTranslate, содержащей переводы на множество языков.
    /// </summary>
    public class ClueTranslations
    {
        public ClueBase Clue { get; set; }

        public int ClueBaseID { get; set; }

        public string Description { get; set; }

        [Key]
        public int ID { get; set; }

        public string LanguageCode { get; set; }
        public string Title { get; set; }
    }
}
