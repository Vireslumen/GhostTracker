namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    ///     Представляет собой информацию о патче, с сылкой на его страницу.
    /// </summary>
    public class Patch
    {
        public int ID { get; set; }
        /// <summary>
        ///     Ссылка на страницу описания патча.
        /// </summary>
        public string Source { get; set; }
        public string Title { get; set; }
    }
}