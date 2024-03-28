using System.ComponentModel.DataAnnotations;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы AchievementTranslations, содержащей переводы на множество языков.
    /// </summary>
    public class AchievementTranslations
    {
        [Key] public int ID { get; set; }
        public string Description { get; set; }
        public string LanguageCode { get; set; }
        public string Tip { get; set; }
        public string Title { get; set; }
    }
}