using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PhasmophobiaCompanion.Models
{
    public class Ghost : ListItemBase
    {
        public ObservableCollection<CluesStructure> Clues { get; set; }
        public ObservableCollection<string> Strength { get; set; }
        public ObservableCollection<string> Abilities { get; set; }
        public ObservableCollection<string> Weaknesses { get; set; }
        public ObservableCollection<string> SanityHunt { get; set; }
        public ObservableCollection<string> Speed { get; set; }
        public string Identification { get; set; }

    }
}
