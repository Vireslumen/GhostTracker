using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы ClueTranslate, содержащей переводы на множество языков.
    /// </summary>
    public class ClueTranslations
    {
        public ClueBase Clue { get; set; }
        public int ClueBaseId { get; set; }
        [Key] public int Id { get; set; }
        public string Description { get; set; }
        public string LanguageCode { get; set; }
        public string Title { get; set; }
    }
}