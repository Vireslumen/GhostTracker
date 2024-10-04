using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы ClueBase.
    /// </summary>
    public class ClueBase
    {
        public ICollection<ClueTranslations> Translations { get; set; }
        [Key] public int Id { get; set; }
        public List<EquipmentBase> EquipmentBase { get; set; }
        public List<ExpandFieldWithImagesBase> ExpandFieldWithImagesBase { get; set; }
        public List<GhostBase> GhostBase { get; set; }
        public List<UnfoldingItemBase> UnfoldingItemBase { get; set; }
        public string IconFilePath { get; set; }
        public string ImageFilePath { get; set; }
    }
}