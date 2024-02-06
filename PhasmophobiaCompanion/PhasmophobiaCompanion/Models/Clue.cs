using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    /// Представляет собой улику, которую оставляют призраки.
    /// </summary>
    public class Clue : BaseDisplayableItem
    {
        public Clue()
        {
            Ghosts = new ObservableCollection<Ghost>();
        }
        public int ID { get; set; }
        // Путь к файлу с иконкой данной улики.
        public string IconFilePath { get; set; }
        public ObservableCollection<ExpandFieldWithImages> ExpandFieldsWithImages { get; set; }
        public List<int> GhostsID { get; set; }
        public ObservableCollection<Ghost> Ghosts { get; set; }
        public ObservableCollection<UnfoldingItem> UnfoldingItems { get; set; }
        public void PopulateAssociatedGhosts(ObservableCollection<Ghost> allghosts)
        {
            foreach (var ghostId in GhostsID)
            {
                var ghost = allghosts.FirstOrDefault(c => c.ID == ghostId);
                if (ghost != null)
                {
                    Ghosts.Add(ghost);
                }
            }
        }
    }
}
