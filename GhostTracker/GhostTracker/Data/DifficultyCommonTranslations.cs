using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы DifficultyCommonTranslations.
    /// </summary>
    public class DifficultyCommonTranslations
    {
        [Key] public int Id { get; set; }
        public string ActivityMonitorWork { get; set; }
        public string DeadCashBack { get; set; }
        public string DifficultiesTitle { get; set; }
        public string DifficultyParams { get; set; }
        public string DifficultyTitle { get; set; }
        public string DoorOpenedCount { get; set; }
        public string ElectricityBlockNotShowedOnMap { get; set; }
        public string ElectricityOn { get; set; }
        public string EvidenceAvailable { get; set; }
        public string FingerPrints { get; set; }
        public string GhostActivity { get; set; }
        public string GhostHuntTime { get; set; }
        public string HidingSpotBlocked { get; set; }
        public string HuntExtendByKilling { get; set; }
        public string IsCursedAvailable { get; set; }
        public string LanguageCode { get; set; }
        public string ObjectiveBoardPendingAloneAll { get; set; }
        public string RewardMultiplier { get; set; }
        public string SanityConsumption { get; set; }
        public string SanityMonitorWork { get; set; }
        public string SanityRestoration { get; set; }
        public string SanityStartAt { get; set; }
        public string SetupTime { get; set; }
        public string UnlockLevel { get; set; }
    }
}