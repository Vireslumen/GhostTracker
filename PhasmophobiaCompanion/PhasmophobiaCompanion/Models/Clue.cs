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
                Ghosts = new List<Ghost>();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации оболочки вкладок.");
                throw;
            }
        }

        public int ID { get; set; }
        public List<int> EquipmentsID { get; set; }
        public List<int> GhostsID { get; set; }
        public List<Equipment> ClueRelatedEquipments { get; set; }
        public List<ExpandFieldWithImages> ExpandFieldsWithImages { get; set; }
        public List<Ghost> Ghosts { get; set; }
        public List<UnfoldingItem> UnfoldingItems { get; set; }
        public string IconFilePath { get; set; }

        /// <summary>
        ///     Связывает улики - Clue с призраками Ghost через имеющийся список Id призраков - GhostsID.
        /// </summary>
        /// <param name="allghosts">Список всех призраков Ghost.</param>
        public void PopulateAssociatedGhosts(List<Ghost> allghosts)
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