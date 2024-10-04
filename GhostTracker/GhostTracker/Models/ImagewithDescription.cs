namespace GhostTracker.Models
{
    /// <summary>
    ///     Представляет собой изображение с описанием к нему.
    /// </summary>
    public class ImageWithDescription
    {
        public string Description { get; set; }
        public string ImageFilePath { get; set; }
    }
}