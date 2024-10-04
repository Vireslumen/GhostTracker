using GhostTracker.Interfaces;

namespace GhostTracker.Models
{
    /// <summary>
    ///     Предоставляет базовую реализацию для элементов, которые могут быть отображены в приложении.
    ///     Может быть расширен для создания специфичных типов элементов.
    /// </summary>
    public abstract class BaseDisplayableItem : IDisplayableItem
    {
        public string Description { get; set; }
        public string ImageFilePath { get; set; }
        public string Title { get; set; }
    }
}