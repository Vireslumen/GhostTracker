using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы ExpandFieldWithImagesBase.
    /// </summary>
    public class ExpandFieldWithImagesBase
    {
        public ICollection<ExpandFieldWithImagesTranslations> Translations { get; set; }
        [Key] public int Id { get; set; }
        public List<ClueBase> ClueBase { get; set; }
        public List<CursedPossessionBase> CursedPossessionBase { get; set; }
        public List<ImageWithDescriptionBase> ImageWithDescriptionBase { get; set; }
        public List<MapBase> MapBase { get; set; }
        public List<OtherInfoBase> OtherInfoBase { get; set; }
    }
}