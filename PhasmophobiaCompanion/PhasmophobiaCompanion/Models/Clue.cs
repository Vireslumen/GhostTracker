using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace GhostTracker.Models
{
    /// <summary>
    ///     Представляет собой улику, которую оставляют призраки.
    /// </summary>
    public class Clue : BaseDisplayableItem, INotifyPropertyChanged
    {
        private string iconFilePath;

        public Clue()
        {
            App.ThemeChanged += HandleThemeChanged;
            Ghosts = new List<Ghost>();
        }

        public int ID { get; set; }
        public List<Equipment> ClueRelatedEquipments { get; set; }
        public List<ExpandFieldWithImages> ExpandFieldsWithImages { get; set; }
        public List<Ghost> Ghosts { get; set; }
        public List<int> EquipmentsID { get; set; }
        public List<int> GhostsID { get; set; }
        public List<UnfoldingItem> UnfoldingItems { get; set; }
        public string IconFilePath { get; set; }
        public string ThemedIconFilePath => GetThemedIcon();
        public event PropertyChangedEventHandler PropertyChanged;

        private string GetThemedIcon()
        {
            var themePrefix = Application.Current.UserAppTheme == OSAppTheme.Dark ||
                              (Application.Current.UserAppTheme == OSAppTheme.Unspecified &&
                               Application.Current.RequestedTheme == OSAppTheme.Dark)
                ? "dark_"
                : "";
            return themePrefix + IconFilePath;
        }

        private void HandleThemeChanged()
        {
            OnPropertyChanged(nameof(ThemedIconFilePath));
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Связывает улики - Clue с призраками Ghost через имеющийся список Id призраков - GhostsID.
        /// </summary>
        /// <param name="allghosts">Список всех призраков Ghost.</param>
        public void PopulateAssociatedGhosts(List<Ghost> allghosts)
        {
            foreach (var ghostId in GhostsID)
            {
                var ghost = allghosts.FirstOrDefault(c => c.ID == ghostId);
                if (ghost != null) Ghosts.Add(ghost);
            }
        }
    }
}