﻿using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы OtherEquipmentStatBase, содержащей переводы на множество языков.
    /// </summary>
    public class OtherEquipmentStatBase
    {
        [Key] public int Id { get; set; }
        public string LanguageCode { get; set; }
        public string Stat { get; set; }
        public string ShortStat { get; set; }
    }
}