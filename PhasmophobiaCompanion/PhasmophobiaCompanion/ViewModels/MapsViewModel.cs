using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using PhasmophobiaCompanion.Interfaces;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Services;
using PhasmophobiaCompanion.Views;
using Rg.Plugins.Popup.Services;
using Serilog;
using Xamarin.Forms;

namespace PhasmophobiaCompanion.ViewModels
{
    /// <summary>
    ///     ViewModel для страницы списка карт, поддерживающий поиск и фильтрацию.
    /// </summary>
    public class MapsViewModel : BaseViewModel, ISearchable, IFilterable
    {
        private readonly DataService dataService;
        private readonly ObservableCollection<Map> maps;
        private double maxRoom = 100;
        private double minRoom;
        private MapCommon mapCommon;
        private ObservableCollection<Map> filteredMaps;
        private ObservableCollection<string> allSizes;
        private ObservableCollection<string> selectedSizes;
        private string searchQuery;

        public MapsViewModel()
        {
            try
            {
                dataService = DependencyService.Get<DataService>();
                // Загрузка всех карт.
                maps = dataService.GetMaps();
                Maps = new ObservableCollection<Map>(maps);
                MapCommon = dataService.GetMapCommon();
                allSizes = new ObservableCollection<string>
                {
                    "Small",
                    "Medium",
                    "Large"
                };
                AllSizes = new ObservableCollection<string>(allSizes);
                SelectedSizes = new ObservableCollection<string>();
                // Инициализация команд
                SearchCommand = new Command<string>(OnSearchCompleted);
                MapSelectedCommand = new Command<Map>(OnMapSelected);
                FilterCommand = new Command(OnFilterTapped);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации MapsViewModel.");
                throw;
            }
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
        public ICommand FilterCommand { get; private set; }
        public ICommand MapSelectedCommand { get; private set; }
        /// <summary>
        ///     Общие текстовые данные для интерфейса относящегося к картам.
        /// </summary>
        public MapCommon MapCommon
        {
            get => mapCommon;
            set
            {
                mapCommon = value;
                OnPropertyChanged();
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
            try
            {
                //Фильтрация по размеру карты.
                var filteredSize = maps.Where(m =>
                    !SelectedSizes.Any() || SelectedSizes.Any(selectedSize => m.Size == selectedSize)).ToList();
                //Фильтрация по количеству комнат на карте.
                var filteredRoom = filteredSize
                    .Where(m => MinRoom <= m.RoomCount && MaxRoom >= m.RoomCount).ToList();
                Maps = new ObservableCollection<Map>(filteredRoom);
                //Загрузка данных для интерфейса.
                MapCommon = dataService.GetMapCommon();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время фильтрации карт.");
                throw;
            }
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
        public ICommand SearchCommand { get; }

        /// <summary>
        ///     Установка поискового запроса и активация поиска.
        /// </summary>
        /// <param name="query">Поисковый запрос.</param>
        public void OnSearchCompleted(string query)
        {
            try
            {
                SearchQuery = query;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время установки поискового запроса MapsViewModel.");
                throw;
            }
        }

        /// <summary>
        ///     Открытие страницы фильтра карт.
        /// </summary>
        private async void OnFilterTapped()
        {
            try
            {
                // Логика для открытия страницы фильтра
                var filterPage = new FilterMapPage(this);
                await PopupNavigation.Instance.PushAsync(filterPage);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время открытия фильтра на странице карт MapsPage.");
                throw;
            }
        }

        /// <summary>
        ///     Переход на подробную страницу выбранной карты.
        /// </summary>
        /// <param name="selectedMap">Выбранная карта.</param>
        private async void OnMapSelected(Map selectedMap)
        {
            try
            {
                if (selectedMap != null)
                {
                    // Логика для открытия страницы деталей карты
                    var detailPage = new MapDetailPage(selectedMap);
                    await Application.Current.MainPage.Navigation.PushAsync(detailPage);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Ошибка во время перехода на подробную страницу карты из страницы карт MapsPage.");
                throw;
            }
        }

        /// <summary>
        ///     Фильтрация коллекции в соответствии с поисковым запросом.
        /// </summary>
        private void SearchMaps()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SearchQuery))
                {
                    Maps = new ObservableCollection<Map>(maps);
                }
                else
                {
                    //Поиск по названию проклятого карты.
                    var filtered = maps
                        .Where(m => m.Title.ToLowerInvariant().Contains(SearchQuery.ToLowerInvariant())).ToList();
                    Maps = new ObservableCollection<Map>(filtered);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время поиска карт.");
                throw;
            }
        }
    }
}