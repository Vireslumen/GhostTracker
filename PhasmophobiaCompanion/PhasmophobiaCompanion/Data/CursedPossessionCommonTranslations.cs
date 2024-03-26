namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы CursedPossessionCommonTranslations.
    /// </summary>
    public class CursedPossessionCommonTranslations
    {
        public int ID { get; set; }
        public string CursedsTitle { get; set; }
        public string EmptyView { get; set; }
        public string LanguageCode { get; set; }
        public string Search { get; set; }
    }
}