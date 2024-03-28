using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы AchievementBase.
    /// </summary>
    public class AchievementBase
    {
        [Key] public int ID { get; set; }
        public string ImageFilePath { get; set; }
        public ICollection<AchievementTranslations> Translations { get; set; }
    }
}