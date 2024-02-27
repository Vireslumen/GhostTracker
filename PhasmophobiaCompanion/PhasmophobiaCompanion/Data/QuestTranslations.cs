using System.ComponentModel.DataAnnotations;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы QuestTranslations, содержащей переводы на множество языков.
    /// </summary>
    public class QuestTranslations
    {
        [Key] public int ID { get; set; }
        public int QuestBaseID { get; set; }
        public string Clause { get; set; }
        public string Description { get; set; }
        public string LanguageCode { get; set; }
        public string Tip { get; set; }
        public string Type { get; set; }
    }
}