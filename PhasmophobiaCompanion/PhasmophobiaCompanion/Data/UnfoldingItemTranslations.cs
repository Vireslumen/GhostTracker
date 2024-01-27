using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    /// Entity framework модель для таблицы UnfoldingItemTranslations, содержащей переводы на множество языков.
    /// </summary>
    public class UnfoldingItemTranslations
    {
        public string Body { get; set; }

        public string Header { get; set; }

        [Key]
        public int ID { get; set; }

        public string LanguageCode { get; set; }
        public string Title { get; set; }
        public UnfoldingItemBase UnfoldingItem { get; set; }
        public int UnfoldingItemBaseID { get; set; }
    }
}
