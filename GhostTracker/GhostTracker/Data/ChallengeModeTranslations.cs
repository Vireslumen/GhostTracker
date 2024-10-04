using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы ChallengeModeTranslations, содержащей переводы на множество языков.
    /// </summary>
    public class ChallengeModeTranslations
    {
        public int ChallengeModeBaseID { get; set; }
        [Key] public int ID { get; set; }
        public string Description { get; set; }
        public string LanguageCode { get; set; }
        public string Title { get; set; }
        public string Parameters { get; set; }
    }
}