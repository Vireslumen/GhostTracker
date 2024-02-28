namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы QuestCommonTranslations.
    /// </summary>
    public class QuestCommonTranslations
    {
        public int ID { get; set; }
        public string Daily { get; set; }
        public string LanguageCode { get; set; }
        public string Title { get; set; }
        public string Weekly { get; set; }
        public string Description { get; set; }
    }
}