using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы AppShellCommonTranslations.
    /// </summary>
    public class AppShellCommonTranslations
    {
        [Key] public int Id { get; set; }
        public string CursedPossessions { get; set; }
        public string Equipments { get; set; }
        public string Ghosts { get; set; }
        public string LanguageCode { get; set; }
        public string Main { get; set; }
        public string Maps { get; set; }
    }
}