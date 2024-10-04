namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы CursedPossessionCommonTranslations.
    /// </summary>
    public class CursedPossessionCommonTranslations
    {
        public int Id { get; set; }
        public string CursedsTitle { get; set; }
        public string EmptyView { get; set; }
        public string LanguageCode { get; set; }
        public string Search { get; set; }
    }
}