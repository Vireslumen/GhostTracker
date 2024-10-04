namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы ChallengeModeCommonTranslations.
    /// </summary>
    public class ChallengeModeCommonTranslations
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string DiffucltyParams { get; set; }
        public string EquipmentProvided { get; set; }
        public string LanguageCode { get; set; }
        public string Title { get; set; }
    }
}