using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    /// Представляет собой особый режим игры.
    /// </summary>
    public class ChallengeMode
    {
        // Карта особого режима игры.
        public Map ChallengeModeMap { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        // Снаряжение особого режима.
        public List<Equipment> Equipments { get; set; }

        // Настройки сложности для особого режима.
        public Difficulty ChallengeModeDifficulty { get; set; }
    }
}
