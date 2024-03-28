namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    ///     Представляет собой достижение, получаемое в игре.
    /// </summary>
    public class Achievement : BaseDisplayableItem
    {
        /// <summary>
        ///     Подсказка по получению достижения.
        /// </summary>
        public string Tip { get; set; }
    }
}