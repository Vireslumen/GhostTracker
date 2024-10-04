using System.Collections.Generic;
using System.Linq;

namespace GhostTracker.Models
{
    /// <summary>
    ///     Представляет собой призрака с характерными ему свойствами.
    /// </summary>
    public class Ghost : BaseDisplayableItem
    {
        public Ghost()
        {
            Clues = new List<Clue>();
        }

        public int ID { get; set; }
        /// <summary>
        ///     Максимальная скорость призрака без учёта LoS при определенных условиях MaxGhostSpeedClause.
        /// </summary>
        public double MaxGhostSpeed { get; set; }
        /// <summary>
        ///     Максимальная скорость призрака, если призрак видит игрока (LoS) при определенных условиях MaxGhostSpeedLoSClause.
        /// </summary>
        public double MaxGhostSpeedLoS { get; set; }
        /// <summary>
        ///     Пороговое значение рассудка для начала охоты. Охота может начаться, когда уровень рассудка игрока упадет ниже этого
        ///     значения.
        ///     Этот порог активен в определенных условиях игры - MaxSanityHuntClause.
        /// </summary>
        public int MaxSanityHunt { get; set; }
        /// <summary>
        ///     Минимальная скорость призрака, при определенных условиях - MinGhostSpeedClause.
        /// </summary>
        public double MinGhostSpeed { get; set; }
        /// <summary>
        ///     Альтернативное пороговое значение рассудка, которое всегда меньше или равно MaxSanityHunt.
        ///     Охота также может начаться, когда уровень рассудка игрока упадет ниже этого более низкого значения.
        ///     Этот порог применяется в других специфических условиях игры - MinSanityHuntClause, отличающихся от условий для
        ///     MaxSanityHunt.
        /// </summary>
        public int MinSanityHunt { get; set; }
        public List<Clue> Clues { get; set; }
        public List<int> CluesID { get; set; }
        public List<SpeedRange> SpeedRanges { get; set; }
        public List<UnfoldingItem> UnfoldingItems { get; set; }
        /// <summary>
        ///     Текст о том, как лучше определить этот тип призрака.
        /// </summary>
        public string Identification { get; set; }
        /// <summary>
        ///     Условие максимальной скорости призрака - MaxGhostSpeed без учёта LoS .
        /// </summary>
        public string MaxGhostSpeedClause { get; set; }
        /// <summary>
        ///     Условие максимальной скорости призрака - MaxGhostSpeedLoS, если призрак видит игрока (LoS).
        /// </summary>
        public string MaxGhostSpeedLoSClause { get; set; }
        /// <summary>
        ///     Условия для активации порога рассудка MaxSanityHunt для начала охоты.
        /// </summary>
        public string MaxSanityHuntClause { get; set; }
        /// <summary>
        ///     Условие для минимальной скорости призрака - MinGhostSpeed.
        /// </summary>
        public string MinGhostSpeedClause { get; set; }
        /// <summary>
        ///     Условия для активации порога рассудка MinSanityHunt для начала охоты.
        /// </summary>
        public string MinSanityHuntClause { get; set; }

        /// <summary>
        ///     Связывает призраков - Ghost с уликами Clue через имеющийся список Id улик - CluesID.
        /// </summary>
        /// <param name="allClues">Список всех улик.</param>
        public void PopulateAssociatedClues(List<Clue> allClues)
        {
            foreach (var clueId in CluesID)
            {
                var clue = allClues.FirstOrDefault(c => c.ID == clueId);
                if (clue != null) Clues.Add(clue);
            }
        }
    }
}