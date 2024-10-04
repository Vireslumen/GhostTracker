namespace GhostTracker.Models
{
    /// <summary>
    ///     Представляет собой достижение, получаемое в игре.
    /// </summary>
    public class Achievement : BaseTitledItem
    {
        /// <summary>
        ///     Подсказка по получению достижения.
        /// </summary>
        public string Tip { get; set; }
    }
}