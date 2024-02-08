using System.ComponentModel.DataAnnotations;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы DifficultyTranslations, содержащей переводы на множество языков.
    /// </summary>
    public class DifficultyTranslations
    {
        public int DifficultyBaseID { get; set; }
        [Key] public int ID { get; set; }
        public string DeadCashBack { get; set; }
        public string Description { get; set; }
        public string DoorOpenedCount { get; set; }
        public string ElectricityBlockNotShowedOnMap { get; set; }
        public string FingerPrints { get; set; }
        public string GhostActivity { get; set; }
        public string GhostHuntTime { get; set; }
        public string HidingSpotBlocked { get; set; }
        public string HuntExtendByKilling { get; set; }
        public string LanguageCode { get; set; }
        public string ObjectiveBoardPendingAloneAll { get; set; }
        public string SanityStartAt { get; set; }
        public string Title { get; set; }
    }
}