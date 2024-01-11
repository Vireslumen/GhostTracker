using System;
using System.Collections.Generic;
using System.Text;

namespace PhasmophobiaCompanion.Models
{
    public class Difficulty
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UnlockLvl { get; set; }
        public float RewardMultiplier { get; set; }
        public float SetupTime { get; set; }
        public float SanityConsumption { get; set; }
        public string GhostActivity { get; set; }
        public string GhostHuntTime { get; set; }
        public string SanityRestoration { get; set; }
        public string DoorOpenedCount { get; set; }
        public string DeadCashBack {  get; set; }
        public bool ElectisityOn {  get; set; }
        public string ObjectiveBoardPendingAloneAll { get; set; }
        public bool SanityMonitorWork {  get; set; }
        public bool ActivityMonitorWork { get; set; }
        public string HidingSpotBlocked { get; set; }
        public string ElectisityBlockNotShowedOnMap { get; set; }
        public string HuntExtendByKilling { get; set; }
        public int EvidenceAvailable { get; set; }
        public string FingerPrints {  get; set; }
        public string SanityStartAt { get; set; }
    }
}
