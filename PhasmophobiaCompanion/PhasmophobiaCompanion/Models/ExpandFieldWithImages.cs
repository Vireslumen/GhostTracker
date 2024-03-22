using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    ///     Представляет собой раскрывающееся поле со списком изображений внутри.
    ///     Наследует UnfoldingItem (раскрывающееся поле).
    /// </summary>
    public class ExpandFieldWithImages : UnfoldingItem
    {
        public List<ImageWithDescription> ImageWithDescriptions { get; set; }
    }
}