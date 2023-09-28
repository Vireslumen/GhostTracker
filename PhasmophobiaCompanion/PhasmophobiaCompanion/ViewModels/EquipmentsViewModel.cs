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
    class EquipmentsViewModel : BaseViewModel, ISearchable, IFilterable
    {
        private string searchQuery;
        private ObservableCollection<Equipment> equipments;
        private ObservableCollection<Equipment> filteredEquipments;

        public ObservableCollection<Equipment> Equipments
        {
            get { return filteredEquipments; }
            set
            {
                SetProperty(ref filteredEquipments, value);
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
        public ICommand SearchCommand { get; private set; }
        public EquipmentsViewModel()
        {
            equipments = new ObservableCollection<Equipment>()
            {
                new Equipment
                { Cost=50, Description="dada", ImageUrl="Candle_T2.png", Level=16,MaxLimit=4,Tier="II", Title="Candle", UnlockCost=2000,
                    OtherStats= new ObservableCollection<string>{"Radius:3", "Duration:2 min" } },
                 new Equipment
                { Cost=50, Description="dada", ImageUrl="Parabolic_T1.png", Level=1,MaxLimit=4,Tier="I", Title="Parabolic Microphone", UnlockCost=5000,
                    OtherStats= new ObservableCollection<string>{"Radius:3", "Duration:2 min" } },
                  new Equipment
                { Cost=50, Description="dada", ImageUrl="Med_T1.png", Level=43,MaxLimit=4,Tier="III", Title="Medicament", UnlockCost=6000,
                    OtherStats= new ObservableCollection<string>{"Radius:3", "Duration:2 min", "Duration:2 min" } }
            };
            Equipments = new ObservableCollection<Equipment>(equipments);
            SearchCommand = new Command<string>(query => Search(query));
        }
        public void Filter()
        {
            throw new NotImplementedException();
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
                var filtered = equipments.Where(ghost => ghost.Title.ToLowerInvariant().Contains(SearchQuery.ToLowerInvariant())).ToList();
                Equipments = new ObservableCollection<Equipment>(filtered);
            }
        }
    }
}
