using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using PhasmophobiaCompanion.Interfaces;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Services;
using Xamarin.Forms;

namespace PhasmophobiaCompanion.ViewModels
{
    /// <summary>
    ///     ViewModel для страницы списка карт, поддерживающий поиск и фильтрацию.
    /// </summary>
    public class MapsViewModel : BaseViewModel, ISearchable, IFilterable
    {
        private readonly DataService _dataService;
        private readonly ObservableCollection<Map> maps;
        private double maxRoom = 100;
        private double minRoom;
        private ObservableCollection<Map> filteredMaps;
        private ObservableCollection<string> allSizes;
        private ObservableCollection<string> selectedSizes;
        private string searchQuery;

        public MapsViewModel()
        {
            _dataService = DependencyService.Get<DataService>();
            // Загрузка всех карт.
            maps = _dataService.GetMaps();
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

        public double MaxRoom
        {
            get => maxRoom;
            set
            {
                if (maxRoom != value)
                {
                    maxRoom = value;
                    OnPropertyChanged();
                }
            }
        }
        public double MinRoom
        {
            get => minRoom;
            set
            {
                if (minRoom != value)
                {
                    minRoom = value;
                    OnPropertyChanged();
                }
            }
        }
        /// <summary>
        ///     Отображаемая коллекция карт, которая может быть отфильтрована на основе заданных критериев.
        /// </summary>
        public ObservableCollection<Map> Maps
        {
            get => filteredMaps;
            set => SetProperty(ref filteredMaps, value);
        }
        public ObservableCollection<string> AllSizes
        {
            get => allSizes;
            set => SetProperty(ref allSizes, value);
        }
        public ObservableCollection<string> SelectedSizes
        {
            get => selectedSizes;
            set => SetProperty(ref selectedSizes, value);
        }

        /// <summary>
        ///     Фильтрация списка призраков на основе выбранного размера карты и количества комнат.
        /// </summary>
        public void Filter()
        {
            //Фильтрация по размеру карты.
            var filteredSize = maps.Where(maps =>
                !SelectedSizes.Any() || SelectedSizes.Any(selectedSize => maps.Size == selectedSize)).ToList();
            //Фильтрация по количеству комнат на карте.
            var filteredRoom = filteredSize
                .Where(maps => MinRoom <= maps.RoomCount && MaxRoom >= maps.RoomCount).ToList();
            Maps = new ObservableCollection<Map>(filteredRoom);
        }

        public string SearchQuery
        {
            get => searchQuery;
            set
            {
                SetProperty(ref searchQuery, value);
                SearchMaps();
            }
        }
        public ICommand SearchCommand { get; set; }

        /// <summary>
        ///     Установка поискового запроса и активация поиска.
        /// </summary>
        /// <param name="query">Поисковый запрос.</param>
        public void Search(string query)
        {
            SearchQuery = query;
        }

        /// <summary>
        ///     Фильтрация коллекции в соответствии с поисковым запросом.
        /// </summary>
        private void SearchMaps()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                Maps = new ObservableCollection<Map>(maps);
            }
            else
            {
                //Поиск по названию проклятого карты.
                var filtered = maps
                    .Where(maps => maps.Title.ToLowerInvariant().Contains(SearchQuery.ToLowerInvariant())).ToList();
                Maps = new ObservableCollection<Map>(filtered);
            }
        }
    }
}