using System;
using System.Collections.Generic;
using System.Text;

namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    /// Представляет собой квест на игровые сессии для получения игровой валюты.
    /// </summary>
    public class Quest
    {
        public int ID { get; set; }
        // Условие выполнения квеста.
        public string Clause { get; set; }

        public string Description { get; set; }

        public int Reward { get; set; }
    }
}
