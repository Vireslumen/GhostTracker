using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    /// Entity framework модель для таблицы DifficultyTranslations, содержащей переводы на множество языков.
    /// </summary>
    public class DifficultyTranslations
    {
        public string DeadCashBack { get; set; }

        public string Description { get; set; }

        public int DifficultyBaseID { get; set; }

        public string DoorOpenedCount { get; set; }

        public string ElectricityBlockNotShowedOnMap { get; set; }

        public string FingerPrints { get; set; }

        public string GhostActivity { get; set; }

        public string GhostHuntTime { get; set; }

        public string HidingSpotBlocked { get; set; }

        public string HuntExtendByKilling { get; set; }

        [Key]
        public int ID { get; set; }

        public string LanguageCode { get; set; }
        public string ObjectiveBoardPendingAloneAll { get; set; }
        public string SanityRestoration { get; set; }
        public string SanityStartAt { get; set; }
        public string Title { get; set; }
    }
}
