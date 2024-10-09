using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace GhostTracker.Models
{
    /// <summary>
    ///     Представляет собой доказательство, которую оставляют призраки.
    /// </summary>
    public class Clue : BaseTitledItem, INotifyPropertyChanged
    {
        public Clue()
        {
            App.ThemeChanged += HandleThemeChanged;
            Ghosts = new List<Ghost>();
        }

        public int Id { get; set; }
        public List<Equipment> ClueRelatedEquipments { get; set; }
        public List<ExpandFieldWithImages> ExpandFieldsWithImages { get; set; }
        public List<Ghost> Ghosts { get; set; }
        public List<int> EquipmentsId { get; set; }
        public List<int> GhostsId { get; set; }
        public List<UnfoldingItem> UnfoldingItems { get; set; }
        public string IconFilePath { get; set; }
        public string ThemedIconFilePath => GetThemedIcon();
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Связывает доказательства - Clue с призраками Ghost через имеющийся список Id призраков - GhostsId.
        /// </summary>
        /// <param name="allGhosts">Список всех призраков Ghost.</param>
        public void PopulateAssociatedGhosts(List<Ghost> allGhosts)
        {
            foreach (var ghostId in GhostsId)
            {
                var ghost = allGhosts.FirstOrDefault(c => c.Id == ghostId);
                if (ghost != null) Ghosts.Add(ghost);
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
    }
}