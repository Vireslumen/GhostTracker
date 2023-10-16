using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;

namespace PhasmophobiaCompanion.Models
{
    public class ExpandFieldWithImages
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ObservableCollection<ImagewithDescription> ImageWithDescriptions { get; set; }

    }
}
