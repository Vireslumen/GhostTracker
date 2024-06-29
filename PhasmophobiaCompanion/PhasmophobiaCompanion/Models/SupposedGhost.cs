namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    ///     Представляет собой призрака с шансом, что в игровой сессии выпал он.
    /// </summary>
    public class SupposedGhost
    {
        public Ghost Ghost { get; set; }
        public int Percent { get; set; }
        public int Points { get; set; }
    }
}