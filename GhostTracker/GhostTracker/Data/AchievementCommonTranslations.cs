using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы AchievementCommonTranslations.
    /// </summary>
    public class AchievementCommonTranslations
    {
        [Key] public int Id { get; set; }
        public string Description { get; set; }
        public string LanguageCode { get; set; }
        public string Title { get; set; }
    }
}