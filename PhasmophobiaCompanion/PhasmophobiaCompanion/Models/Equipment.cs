using System;
using System.Collections.Generic;
using System.Text;

namespace PhasmophobiaCompanion.Models
{
    public class Equipment: ListItemBase
    {
        public int Level { get; set; }
        public int Tier { get; set; }
        public int Cost { get; set; }
    }
}
