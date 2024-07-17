using System.ComponentModel.DataAnnotations;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы AppShellCommonTranslations.
    /// </summary>
    public class AppShellCommonTranslations
    {
        [Key] public int ID { get; set; }
        public string CursedPossessions { get; set; }
        public string Equipments { get; set; }
        public string Ghosts { get; set; }
        public string LanguageCode { get; set; }
        public string Main { get; set; }
        public string Maps { get; set; }
    }
}