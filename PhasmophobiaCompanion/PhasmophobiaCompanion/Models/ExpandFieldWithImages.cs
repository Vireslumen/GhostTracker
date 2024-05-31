using System.Collections.Generic;

namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    ///     Представляет собой раскрывающееся поле со списком изображений внутри.
    ///     Наследует UnfoldingItem (раскрывающееся поле).
    /// </summary>
    public class ExpandFieldWithImages : UnfoldingItem
    {
        private List<ImageWithDescription> imageWithDescriptions;
        public override bool CanExpand => base.CanExpand || ImageWithDescriptions?.Count > 0;
        public List<ImageWithDescription> ImageWithDescriptions
        {
            get => imageWithDescriptions;
            set
            {
                imageWithDescriptions = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanExpand));
                OnPropertyChanged(nameof(Icon));
            }
        }
    }
}