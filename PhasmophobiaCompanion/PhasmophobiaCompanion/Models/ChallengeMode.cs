using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    ///     Представляет собой особый режим игры.
    /// </summary>
    public class ChallengeMode
    {
        public Difficulty ChallengeDifficulty { get; set; }
        public int DifficultyID { get; set; }
        public int ID { get; set; }
        public int MapID { get; set; }
        public List<int> EquipmentsID { get; set; }
        public Map ChallengeMap { get; set; }
        public List<Equipment> ChallengeEquipments { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
}