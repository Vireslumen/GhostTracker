using System.ComponentModel.DataAnnotations;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы MainPageCommonTranslations, содержащей переводы на множество языков.
    /// </summary>
    public class MainPageCommonTranslations
    {
        [Key] public int ID { get; set; }
        public string Clue { get; set; }
        public string DailyQuest { get; set; }
        public string Difficulties { get; set; }
        public string LanguageCode { get; set; }
        public string OtherPages { get; set; }
        public string Patches { get; set; }
        public string Search { get; set; }
        public string Settings { get; set; }
        public string SpecialMode { get; set; }
        public string Theme { get; set; }
        public string Tip { get; set; }
        public string WeeklyQuest { get; set; }
    }
}