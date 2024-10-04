using GhostTracker.Interfaces;

namespace GhostTracker.Models
{
    /// <summary>
    ///     Класс AchievementCommon содержит атрибуты для локализации текстовых элементов интерфейса,
    ///     относящихся к достижениям - Achievement.
    /// </summary>
    public class AchievementCommon : ITitledItem
    {
        public string Description { get; set; }
        public string Title { get; set; }
    }
}