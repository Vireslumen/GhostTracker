namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    ///     Представляет собой подсказку к игре.
    /// </summary>
    public class Tip
    {
        /// <summary>
        ///     Опредяляет какому уровню игроков будет полезна подсказка.
        /// </summary>
        public string Level { get; set; }
        public string TipValue { get; set; }
    }
}