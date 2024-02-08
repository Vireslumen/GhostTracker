using System.ComponentModel.DataAnnotations;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы TipsTranslations, содержащей подсказки и их переводы на множество языков.
    /// </summary>
    public class TipsTranslations
    {
        [Key] public int ID { get; set; }
        public string LanguageCode { get; set; }
        public string Tip { get; set; }
    }
}