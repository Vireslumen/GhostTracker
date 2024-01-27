using AutoFixture;
using PhasmophobiaCompanion.Interfaces;
using PhasmophobiaCompanion.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PhasmophobiaCompanion.ViewModels
{
    public class MapsViewModel : BaseViewModel, ISearchable, IFilterable
    {
        public string SearchQuery
        {
            get { return searchQuery; }
            set
            {
                SetProperty(ref searchQuery, value);
                SearchMaps();
            }
        }

        private void SearchMaps()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                Maps = new ObservableCollection<Map>(maps);
            }
            else
            {
                var filtered = maps.Where(maps => maps.Title.ToLowerInvariant().Contains(SearchQuery.ToLowerInvariant())).ToList();
                Maps = new ObservableCollection<Map>(filtered);
            }
        }

        private string searchQuery;
        public ICommand SearchCommand { get; set; }
        private ObservableCollection<Map> maps;
        private ObservableCollection<Map> filteredMaps;
        public ObservableCollection<Map> Maps
        {
            get { return filteredMaps; }
            set
            {
                SetProperty(ref filteredMaps, value);
            }
        }

        public MapsViewModel()
        {
            maps = new ObservableCollection<Map>();
            Maps = new ObservableCollection<Map>(maps);
            allSizes = new ObservableCollection<string>
            {
                "Small",
                "Medium",
                "Large"
            };
            AllSizes = new ObservableCollection<string>(allSizes);
            SelectedSizes = new ObservableCollection<string>();
            SearchCommand = new Command<string>(query => Search(query));
        }

        private double minRoom = 0;
        public double MinRoom
        {
            get { return minRoom; }
            set
            {
                if (minRoom != value)
                {
                    minRoom = value;
                    OnPropertyChanged(nameof(MinRoom));
                }
            }
        }

        private double maxRoom = 100;
        public double MaxRoom
        {
            get { return maxRoom; }
            set
            {
                if (maxRoom != value)
                {
                    maxRoom = value;
                    OnPropertyChanged(nameof(MaxRoom));
                }
            }
        }
        private ObservableCollection<string> allSizes;

        public ObservableCollection<string> AllSizes
        {
            get { return allSizes; }
            set
            {
                SetProperty(ref allSizes, value);
            }
        }
        private ObservableCollection<string> selectedSizes;
        public ObservableCollection<string> SelectedSizes
        {
            get { return selectedSizes; }
            set
            {
                SetProperty(ref selectedSizes, value);
            }
        }
        public void Filter()
        {
            var filteredSize = maps.Where(maps => (!SelectedSizes.Any() || SelectedSizes.Any(selectedSize => maps.Size == selectedSize))).ToList();
            var filteredRoom = filteredSize.Where(maps => ((MinRoom <= maps.RoomCount && MaxRoom >= maps.RoomCount))).ToList();
            Maps = new ObservableCollection<Map>(filteredRoom);
        }

        public void Search(string query)
        {
            SearchQuery = query;
        }
    }
}
