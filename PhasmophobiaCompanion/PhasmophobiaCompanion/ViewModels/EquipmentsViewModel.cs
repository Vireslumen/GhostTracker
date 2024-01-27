using PhasmophobiaCompanion.Interfaces;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PhasmophobiaCompanion.ViewModels
{
    public class EquipmentsViewModel : BaseViewModel, ISearchable, IFilterable
    {
        private string searchQuery;
        private ObservableCollection<Equipment> equipments;
        private ObservableCollection<Equipment> filteredEquipments;
        private ObservableCollection<string> allTiers;

        public ObservableCollection<string> AllTiers
        {
            get { return allTiers; }
            set
            {
                SetProperty(ref allTiers, value);
            }
        }
        private ObservableCollection<string> selectedTiers;

        public ObservableCollection<string> SelectedTiers
        {
            get { return selectedTiers; }
            set
            {
                SetProperty(ref selectedTiers, value);
            }
        }
        public ObservableCollection<Equipment> Equipments
        {
            get { return filteredEquipments; }
            set
            {
                SetProperty(ref filteredEquipments, value);
            }
        }

        private double minUnlockLevel = 0;
        public double MinUnlockLevel
        {
            get { return minUnlockLevel; }
            set
            {
                if (minUnlockLevel != value)
                {
                    minUnlockLevel = value;
                    OnPropertyChanged(nameof(MinUnlockLevel));
                }
            }
        }

        private double maxUnlockLevel = 100;
        public double MaxUnlockLevel
        {
            get { return maxUnlockLevel; }
            set
            {
                if (maxUnlockLevel != value)
                {
                    maxUnlockLevel = value;
                    OnPropertyChanged(nameof(MaxUnlockLevel));
                }
            }
        }

        public string SearchQuery
        {
            get { return searchQuery; }
            set
            {
                SetProperty(ref searchQuery, value);
                SearchEquipments();
            }
        }
        public ICommand SearchCommand { get; set; }
        public EquipmentsViewModel()
        {
            allTiers = new ObservableCollection<string>
            {
                "I",
                "II",
                "III"
            };
            equipments = new ObservableCollection<Equipment>();
            AllTiers = new ObservableCollection<string>(allTiers);
            Equipments = new ObservableCollection<Equipment>(equipments);
            SelectedTiers = new ObservableCollection<string>();
            SearchCommand = new Command<string>(query => Search(query));
        }
        public void Filter()
        {
            var filteredTier = equipments.Where(equipment => (!SelectedTiers.Any() || SelectedTiers.Any(selectedTier => equipment.Tier == selectedTier))).ToList();
            var filteredLevel = filteredTier.Where(equipment => ((MinUnlockLevel <= equipment.UnlockLevel && MaxUnlockLevel >= equipment.UnlockLevel))).ToList();

            Equipments = new ObservableCollection<Equipment>(filteredLevel);
        }


        public void Search(string query)
        {
            SearchQuery = query;
        }
        private void SearchEquipments()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                Equipments = new ObservableCollection<Equipment>(equipments);
            }
            else
            {
                var filtered = equipments.Where(equipment => equipment.Title.ToLowerInvariant().Contains(SearchQuery.ToLowerInvariant())).ToList();
                Equipments = new ObservableCollection<Equipment>(filtered);
            }
        }
    }
}
