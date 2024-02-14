using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Serilog;

namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    ///     Представляет собой улику, которую оставляют призраки.
    /// </summary>
    public class Clue : BaseDisplayableItem
    {
        public Clue()
        {
            try
            {
                Ghosts = new ObservableCollection<Ghost>();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации оболочки вкладок.");
                throw;
            }
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
            try
            {
                foreach (var ghostId in GhostsID)
                {
                    var ghost = allghosts.FirstOrDefault(c => c.ID == ghostId);
                    if (ghost != null) Ghosts.Add(ghost);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время связывания улик с призраками.");
                throw;
            }
        }
    }
}