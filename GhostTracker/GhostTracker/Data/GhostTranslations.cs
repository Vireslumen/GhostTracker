using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы GhostTranslations, содержащей переводы на множество языков.
    /// </summary>
    public class GhostTranslations
    {
        public GhostBase Ghost { get; set; }
        public int GhostBaseId { get; set; }
        [Key] public int Id { get; set; }
        public string Description { get; set; }
        public string Identification { get; set; }
        public string LanguageCode { get; set; }
        public string MaxGhostSpeedClause { get; set; }
        public string MaxGhostSpeedLoSClause { get; set; }
        public string MaxSanityHuntClause { get; set; }
        public string MinGhostSpeedClause { get; set; }
        public string MinSanityHuntClause { get; set; }
        public string Title { get; set; }
    }
}