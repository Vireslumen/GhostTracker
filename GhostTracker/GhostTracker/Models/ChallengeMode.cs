using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GhostTracker.Models
{
    /// <summary>
    ///     Представляет собой особый режим игры.
    /// </summary>
    public class ChallengeMode
    {
        public int DifficultyId { get; set; }
        public int Id { get; set; }
        public int MapId { get; set; }
        public List<int> EquipmentsId { get; set; }
        public Map ChallengeMap { get; set; }
        public List<Equipment> ChallengeEquipments { get; set; }
        public string Description { get; set; }
        public string Parameters { get; set; }
        public string Title { get; set; }
    }
}