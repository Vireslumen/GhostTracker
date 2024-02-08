using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы GhostBase.
    /// </summary>
    public class GhostBase
    {
        public ICollection<GhostTranslations> Translations { get; set; }
        [Key] public int ID { get; set; }
        public int MaxGhostSpeed { get; set; }
        public int MaxGhostSpeedLoS { get; set; }
        public int MaxSanityHunt { get; set; }
        public int MinGhostSpeed { get; set; }
        public int MinSanityHunt { get; set; }
        public ObservableCollection<ClueBase> ClueBase { get; set; }
        public ObservableCollection<UnfoldingItemBase> UnfoldingItemBase { get; set; }
        public string ImageFilePath { get; set; }
    }
}