using PhasmophobiaCompanion.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    /// Entity framework модель для таблицы ExpandFieldWithImagesBase.
    /// </summary>
    public class ExpandFieldWithImagesBase
    {
        public ObservableCollection<ClueBase> ClueBase { get; set; }
        public ObservableCollection<CursedPossessionBase> CursedPossessionBase { get; set; }

        [Key]
        public int ID { get; set; }

        public ObservableCollection<ImageWithDescriptionBase> ImageWithDescriptionBase { get; set; }
        public ObservableCollection<MapBase> MapBase { get; set; }
        public ObservableCollection<OtherInfoBase> OtherInfoBase { get; set; }
        public ICollection<ExpandFieldWithImagesTranslations> Translations { get; set; }
    }
}
