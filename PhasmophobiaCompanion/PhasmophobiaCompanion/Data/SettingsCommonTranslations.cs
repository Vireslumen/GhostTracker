using System.ComponentModel.DataAnnotations;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы SettingsCommonTranslations.
    /// </summary>
    public class SettingsCommonTranslations
    {
        [Key] public int ID { get; set; }
        public string About { get; set; }
        public string AnyLevel { get; set; }
        public string AppLanguage { get; set; }
        public string ErrorReport { get; set; }
        public string LanguageCode { get; set; }
        public string LoggerServerActive { get; set; }
        public string SelectedLevel { get; set; }
        public string SelectLanguage { get; set; }
        public string SelectLevel { get; set; }
        public string SettingsTitle { get; set; }
        public string ShakeActiveLabel { get; set; }
        public string TipLevel { get; set; }
    }
}