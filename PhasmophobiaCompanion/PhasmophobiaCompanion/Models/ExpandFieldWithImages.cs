using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;

namespace PhasmophobiaCompanion.Models
{
    public class ExpandFieldWithImages: UnfoldingItem
    {
        public ObservableCollection<ImagewithDescription> ImageWithDescriptions { get; set; }

    }
}
