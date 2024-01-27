using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    /// Представляет собой призрака с характерными ему свойствами.
    /// </summary>
    public class Ghost : BaseDisplayableItem
    {
        public ObservableCollection<Clue> Clues { get; set; }

        // Как лучше определить этот тип призрака.
        public string Identification { get; set; }

        // Максимальная скорость призрака.
        public int MaxGhostSpeed { get; set; }

        // Условие максимальной скорости призрака.
        public string MaxGhostSpeedClause { get; set; }

        // Максимальная скорость призрака, если призрак видит игрока.
        public int MaxGhostSpeedLoS { get; set; }

        // Условие максимальной скорости призрака, если призрак видит игрока.
        public string MaxGhostSpeedLoSClause { get; set; }

        // Максимальное значение, с которого может начаться охота.
        public int MaxSanityHunt { get; set; }

        // Условие максимального значения, с которого может начаться охота.
        public string MaxSanityHuntClause { get; set; }

        // Минимальная скорость призрака.
        public int MinGhostSpeed { get; set; }

        // Условие для минимальной скорости призрака.
        public string MinGhostSpeedClause { get; set; }

        // Минимальное значение рассудка, до которого охота не может начаться.
        public int MinSanityHunt { get; set; }

        // Условие минимального значение рассудка, до которого охота не может начаться.
        public string MinSanityHuntClause { get; set; }
        public ObservableCollection<UnfoldingItem> UnfoldingItems { get; set; }
    }
}