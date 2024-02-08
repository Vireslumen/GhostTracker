using System.Collections.ObjectModel;

namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    ///     Представляет собой раскрывающееся поле со списком изображений внутри.
    ///     Наследует UnfoldingItem (раскрывающееся поле).
    /// </summary>
    public class ExpandFieldWithImages : UnfoldingItem
    {
        public ObservableCollection<ImageWithDescription> ImageWithDescriptions { get; set; }
    }
}