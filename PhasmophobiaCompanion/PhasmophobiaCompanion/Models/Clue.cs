using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    /// Представляет собой улику, которую оставляют призраки.
    /// </summary>
    public class Clue : BaseDisplayableItem
    {
        public int ID { get; set; }
        // Путь к файлу с иконкой данной улики.
        public string IconFilePath { get; set; }
        public ObservableCollection<ExpandFieldWithImages> ExpandFieldsWithImages { get; set; }
        public List<int> GhostsID { get; set; }
        public ObservableCollection<UnfoldingItem> UnfoldingItems { get; set; }
    }
}
