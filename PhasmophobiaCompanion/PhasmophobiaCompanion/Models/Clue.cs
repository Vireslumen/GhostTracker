using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    ///     Представляет собой улику, которую оставляют призраки.
    /// </summary>
    public class Clue : BaseDisplayableItem
    {
        public Clue()
        {
            Ghosts = new ObservableCollection<Ghost>();
        }

        public int ID { get; set; }
        public List<int> GhostsID { get; set; }
        public ObservableCollection<ExpandFieldWithImages> ExpandFieldsWithImages { get; set; }
        public ObservableCollection<Ghost> Ghosts { get; set; }
        public ObservableCollection<UnfoldingItem> UnfoldingItems { get; set; }
        public string IconFilePath { get; set; }

        /// <summary>
        ///     Связывает улики - Clue с призраками Ghost через имеющийся список Id призраков - GhostsID.
        /// </summary>
        /// <param name="allghosts">Список всех призраков Ghost.</param>
        public void PopulateAssociatedGhosts(ObservableCollection<Ghost> allghosts)
        {
            foreach (var ghostId in GhostsID)
            {
                var ghost = allghosts.FirstOrDefault(c => c.ID == ghostId);
                if (ghost != null) Ghosts.Add(ghost);
            }
        }
    }
}