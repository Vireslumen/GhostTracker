using System;
using System.Collections.Generic;
using System.Text;

namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    /// Представляет собой общие данные для всех видов снаряжения.
    /// </summary>
    public class EquipmentCommon
    {
        public string EquipmentsTitle { get; set; }
        public string Tier { get; set; }
        public string Search { get; set; }
        public string FilterTier { get; set; }
        public string FilterUnlock { get; set; }
        public string Price { get; set; }
        public string PriceUnlock { get; set; }
        public string MaxLimit { get; set; }
        public string Apply { get; set; }
        public string UnlockLevel { get; set; }
    }
}
