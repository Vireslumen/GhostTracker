using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PhasmophobiaCompanion.Models
{
    public class ChallengeMode
    {
        public Map ChallengeModeMap { get; set; }
        public string Decription { get; set; }
        public Dictionary<Equipment, int> EquipmentList { get; set; }
        public string SanityStat { get; set; }
        public string ActivityStat { get; set; }
    }
}
