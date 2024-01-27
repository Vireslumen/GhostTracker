using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    /// Entity framework модель для таблицы EquipmentTranslations, содержащей переводы на множество языков.
    /// </summary>
    public class EquipmentTranslations
    {
        public string Description { get; set; }

        public int EquipmentBaseID { get; set; }

        [Key]
        public int ID { get; set; }

        public string LanguageCode { get; set; }
        public string Tier { get; set; }
        public string Title { get; set; }
        public string Uses { get; set; }
    }
}
