using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы GhostCommonTranslations.
    /// </summary>
    public class GhostCommonTranslations
    {
        [Key] public int Id { get; set; }
        public string ApplyTitle { get; set; }
        public string Clear { get; set; }
        public string EmptyView { get; set; }
        public string FilterTitle { get; set; }
        public string GhostsTitle { get; set; }
        public string GhostTitle { get; set; }
        public string LanguageCode { get; set; }
        public string LoS { get; set; }
        public string Max { get; set; }
        public string MaxSanityHunt { get; set; }
        public string MaxSpeed { get; set; }
        public string MaxSpeedLoS { get; set; }
        public string Min { get; set; }
        public string MinSanityHunt { get; set; }
        public string MinSpeed { get; set; }
        public string SanityHunt { get; set; }
        public string Search { get; set; }
        public string Speed { get; set; }
    }
}