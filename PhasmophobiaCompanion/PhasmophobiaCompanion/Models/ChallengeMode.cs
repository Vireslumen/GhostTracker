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
        public int ID { get; set; }

        // ID карты особого режима игры.
        public int MapID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        // Снаряжение особого режима.
        public List<int> EquipmentsID { get; set; }

        // Настройки сложности для особого режима.
        public int DifficultyID { get; set; }

        public Map ChallengeMap { get; set; }
        public Difficulty ChallengeDifficulty { get; set; }
        public ObservableCollection<Equipment> ChallengeEquipments { get; set; }
    }
}
