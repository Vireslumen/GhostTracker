using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GhostTracker.Interfaces;
using GhostTracker.Models;
using GhostTracker.Services;
using GhostTracker.Views;
using Rg.Plugins.Popup.Services;
using Serilog;
using Xamarin.Forms;

namespace GhostTracker.ViewModels
{
    /// <summary>
    ///     ViewModel для страницы списка карт, поддерживающий поиск и фильтрацию.
    /// </summary>
    public class MapsViewModel : SearchableViewModel, IFilterable
    {
        private const int MaxRoomDefault = 100;
        private const int MinRoomDefault = 0;
        private readonly List<Map> maps;
        private int maxRoomSaved;
        private int minRoomSaved;
        private List<object> selectedSizesSaved;
        private List<string> allSizes;
        private MapCommon mapCommon;
        private ObservableCollection<Map> filteredMaps;
        private ObservableCollection<object> selectedSizes;
        private string maxRoom;
        private string minRoom;

        public MapsViewModel()
        {
            var dataService = DependencyService.Get<DataService>();
            // Загрузка всех карт.
            maps = dataService.GetMaps().OrderBy(m => m.SizeNumeric).ThenBy(m => m.UnlockLevel).ToList();
            Maps = new ObservableCollection<Map>(maps);
            MapCommon = dataService.GetMapCommon();
            allSizes = dataService.GetSizes();
            AllSizes = new List<string>(allSizes);
            SelectedSizes = new ObservableCollection<object>();
            selectedSizesSaved = new List<object>();
            maxRoomSaved = MaxRoomDefault;
            minRoomSaved = MinRoomDefault;
            maxRoom = MaxRoomDefault.ToString();
            minRoom = MinRoomDefault.ToString();
            // Инициализация команд
            MapSelectedCommand = new Command<Map>(OnMapSelected);
            FilterCommand = new Command(OnFilterTapped);
            FilterApplyCommand = new Command(OnFilterApplyTapped);
            FilterClearCommand = new Command(OnFilterClearTapped);
            BackgroundClickCommand = new Command(ExecuteBackgroundClick);
        }

        public ICommand BackgroundClickCommand { get; private set; }
        public ICommand FilterApplyCommand { get; private set; }
        public ICommand FilterClearCommand { get; private set; }
        public ICommand FilterCommand { get; private set; }
        public ICommand MapSelectedCommand { get; private set; }
        public List<string> AllSizes
        {
            get => allSizes;
            set => SetProperty(ref allSizes, value);
        }
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
        public ObservableCollection<object> SelectedSizes
        {
            get => selectedSizes;
            set => SetProperty(ref selectedSizes, value);
        }
        public string FilterColor =>
            SelectedSizes.Any() || minRoomSaved != 0 || maxRoomSaved != 100
                ? "#FD7E14"
                : "Transparent";
        public string MaxRoom
        {
            get => maxRoom;
            set
            {
                if (maxRoom == value) return;
                maxRoom = value;
                OnPropertyChanged();
            }
        }
        public string MinRoom
        {
            get => minRoom;
            set
            {
                if (minRoom == value) return;
                minRoom = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Фильтрация списка призраков на основе выбранного размера карты и количества комнат.
        /// </summary>
        public void Filter()
        {
            try
            {
                UpdateFilteredMaps();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время фильтрации карт.");
            }
        }

        /// <summary>
        ///     Фильтрация коллекции в соответствии с поисковым запросом.
        /// </summary>
        protected override void PerformSearch()
        {
            try
            {
                UpdateFilteredMaps();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время поиска карт.");
            }
        }

        /// <summary>
        ///     При нажатии на фон сбросить параметры фильтра до состояния на момента его открытия.
        /// </summary>
        private void ExecuteBackgroundClick()
        {
            try
            {
                SelectedSizes = new ObservableCollection<object>(selectedSizesSaved);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при сбрасывании параметров фильтра до состояния на момент открытия.");
            }
        }

        /// <summary>
        ///     Принятие фильтрации и закрытие окна фильтра.
        /// </summary>
        private void OnFilterApplyTapped()
        {
            try
            {
                selectedSizesSaved = new List<object>(SelectedSizes);
                minRoomSaved = int.TryParse(minRoom, out var max) ? max : 100;
                maxRoomSaved = int.TryParse(maxRoom, out var min) ? min : 0;
                Filter();
                OnPropertyChanged(nameof(FilterColor));
                PopupNavigation.Instance.PopAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при принятии фильтрации.");
            }
        }

        /// <summary>
        ///     Сброс параметров фильтрации и закрытие окна фильтрации.
        /// </summary>
        private void OnFilterClearTapped()
        {
            try
            {
                selectedSizesSaved = new List<object>();
                SelectedSizes = new ObservableCollection<object>();
                maxRoomSaved = MaxRoomDefault;
                minRoomSaved = MinRoomDefault;
                MaxRoom = MaxRoomDefault.ToString();
                MinRoom = MinRoomDefault.ToString();
                Filter();
                OnPropertyChanged(nameof(FilterColor));
                PopupNavigation.Instance.PopAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при сбросе фильтрации.");
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
                if (IsNavigating) return;
                if (selectedMap == null) return;
                // Логика для открытия страницы деталей карты
                var detailPage = new MapDetailPage(selectedMap);
                await NavigateWithLoadingAsync(detailPage);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Ошибка во время перехода на подробную страницу карты из страницы карт MapsPage.");
            }
        }

        /// <summary>
        ///     Фильтрация карт по поисковому запросу и выбранным параметрам фильтра.
        /// </summary>
        private void UpdateFilteredMaps()
        {
            try
            {
                var filteredBySizeAndRoom = maps.Where(map =>
                    (!selectedSizesSaved.Any() ||
                     selectedSizesSaved.Any(selectedSize => map.Size == selectedSize.ToString())) &&
                    minRoomSaved <= map.RoomCount && maxRoomSaved >= map.RoomCount).ToList();

                var finalFiltered = string.IsNullOrWhiteSpace(SearchText)
                    ? filteredBySizeAndRoom
                    : filteredBySizeAndRoom.Where(map =>
                        map.Title.ToLowerInvariant().Contains(SearchText.ToLowerInvariant())).ToList();

                Maps = new ObservableCollection<Map>(finalFiltered);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время обновления отфильтрованного списка карт.");
            }
        }
    }
}