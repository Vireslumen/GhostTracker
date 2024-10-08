﻿using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы EquipmentTranslations, содержащей переводы на множество языков.
    /// </summary>
    public class EquipmentTranslations
    {
        [Key] public int Id { get; set; }
        public string Description { get; set; }
        public string LanguageCode { get; set; }
        public string Tier { get; set; }
        public string Title { get; set; }
    }
}