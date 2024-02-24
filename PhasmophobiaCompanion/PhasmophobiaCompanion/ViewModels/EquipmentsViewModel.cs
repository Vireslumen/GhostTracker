using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
    ///     ViewModel для страницы списка снаряжения, поддерживает поиск и фильтрацию.
    /// </summary>
    public class EquipmentsViewModel : BaseViewModel, ISearchable, IFilterable
    {
        private readonly DataService dataService;
        private readonly ObservableCollection<Equipment> equipments;
        private double maxUnlockLevelSaved = 100;
        private double minUnlockLevelSaved;
        private EquipmentCommon equipmentCommon;
        private ObservableCollection<Equipment> filteredEquipments;
        private ObservableCollection<string> allTiers;
        private ObservableCollection<object> selectedTiers;
        private ObservableCollection<object> selectedTiersSaved;
        private string maxUnlockLevel = "100";
        private string minUnlockLevel = "0";
        private string searchQuery;

        public EquipmentsViewModel()
        {
            try
            {
                dataService = DependencyService.Get<DataService>();
                //TODO: Нужно сделать, чтобы варианты тиров загружались не из кода.
                allTiers = new ObservableCollection<string>
                {
                    "I",
                    "II",
                    "III"
                };
                //Загрузка данных для интерфейса.
                EquipmentCommon = dataService.GetEquipmentCommon();
                //Загрузка всего снаряжения.
                equipments = dataService.GetEquipments();
                AllTiers = new ObservableCollection<string>(allTiers);
                Equipments = new ObservableCollection<Equipment>(equipments);
                SelectedTiers = new ObservableCollection<object>();
                SelectedTiers.CollectionChanged += SelectedTiers_CollectionChanged;
                // Инициализация команд
                SearchCommand = new Command<string>(OnSearchCompleted);
                EquipmentSelectedCommand = new Command<Equipment>(OnEquipmentSelected);
                FilterCommand = new Command(OnFilterTapped);
                FilterApplyCommand = new Command(OnFilterApplyTapped);
                BackgroundClickCommand = new Command(ExecuteBackgroundClick);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации EquipmentsViewModel.");
                throw;
            }
        }

        /// <summary>
        ///     Общие текстовые данные для интерфейса относящегося к снаряжению.
        /// </summary>
        public EquipmentCommon EquipmentCommon
        {
            get => equipmentCommon;
            set
            {
                equipmentCommon = value;
                OnPropertyChanged();
            }
        }
        public ICommand BackgroundClickCommand { get; private set; }
        public ICommand EquipmentSelectedCommand { get; private set; }
        public ICommand FilterApplyCommand { get; private set; }
        public ICommand FilterCommand { get; private set; }
        /// <summary>
        ///     Коллекция отображаемого снаряжения, которая может быть отфильтрована.
        /// </summary>
        public ObservableCollection<Equipment> Equipments
        {
            get => filteredEquipments;
            set => SetProperty(ref filteredEquipments, value);
        }
        /// <summary>
        ///     Коллекция всех доступных тиров оборудования.
        /// </summary>
        public ObservableCollection<string> AllTiers
        {
            get => allTiers;
            set => SetProperty(ref allTiers, value);
        }
        /// <summary>
        ///     Коллекция выбранных тиров для фильтрации.
        /// </summary>
        public ObservableCollection<object> SelectedTiers
        {
            get => selectedTiers;
            set => SetProperty(ref selectedTiers, value);
        }
        public string FilterColor =>
            SelectedTiers.Any() || minUnlockLevelSaved != 0 || maxUnlockLevelSaved != 100 ? "Yellow" : "White";
        public string MaxUnlockLevel
        {
            get => maxUnlockLevel;
            set
            {
                if (maxUnlockLevel != value)
                {
                    maxUnlockLevel = value;
                    OnPropertyChanged();
                }
            }
        }
        public string MinUnlockLevel
        {
            get => minUnlockLevel;
            set
            {
                if (minUnlockLevel != value)
                {
                    minUnlockLevel = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        ///     Фильтрация оборудования по выбранным тирам и диапазону уровней разблокировки.
        /// </summary>
        public void Filter()
        {
            try
            {
                UpdateFilteredEquipments();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время фильтрации снаряжения.");
                throw;
            }
        }

        public string SearchQuery
        {
            get => searchQuery;
            set
            {
                SetProperty(ref searchQuery, value);
                SearchEquipments();
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
                Log.Error(ex, "Ошибка во время установки поискового запроса у EquipmentsViewModel.");
                throw;
            }
        }

        /// <summary>
        ///     При нажатии на фон сбросить параметры фильтра до состояния на момента его открытия.
        /// </summary>
        private void ExecuteBackgroundClick()
        {
            try
            {
                SelectedTiers = new ObservableCollection<object>(selectedTiersSaved);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при сбрасывании параметров фильтра до состояния на момент открытия.");
                throw;
            }
        }

        /// <summary>
        ///     Переход на подробную страницу выбранного снаряжения.
        /// </summary>
        /// <param name="selectedEquipment">Выбранное снаряжение.</param>
        private async void OnEquipmentSelected(Equipment selectedEquipment)
        {
            try
            {
                if (selectedEquipment != null)
                {
                    // Логика для открытия страницы деталей снаряжения
                    var detailPage = new EquipmentDetailPage(selectedEquipment);
                    await Application.Current.MainPage.Navigation.PushAsync(detailPage);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Ошибка во время перехода на подробную страницу снаряжения из страницы снаряжения EquipmentPage.");
                throw;
            }
        }

        /// <summary>
        ///     Принятие фильтрации и закрытие окна фильтра.
        /// </summary>
        private void OnFilterApplyTapped()
        {
            try
            {
                selectedTiersSaved = new ObservableCollection<object>(SelectedTiers);
                minUnlockLevelSaved = double.TryParse(minUnlockLevel, out var max) ? max : 100;
                maxUnlockLevelSaved = double.TryParse(maxUnlockLevel, out var min) ? min : 0;
                Filter();
                OnPropertyChanged(nameof(FilterColor));
                PopupNavigation.Instance.PopAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при принятии фильтрации.");
                throw;
            }
        }

        /// <summary>
        ///     Открытие страницы фильтра снаряжения.
        /// </summary>
        private async void OnFilterTapped()
        {
            try
            {
                // Логика для открытия страницы фильтра
                var filterPage = new FilterEquipmentPage(this);
                await PopupNavigation.Instance.PushAsync(filterPage);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время открытия фильтра на странице снаряжения EquipmentPage.");
                throw;
            }
        }

        /// <summary>
        ///     Фильтрация коллекции в соответствии с поисковым запросом.
        /// </summary>
        private void SearchEquipments()
        {
            try
            {
                UpdateFilteredEquipments();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время поиска снаряжения.");
                throw;
            }
        }

        private void SelectedTiers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(FilterColor));
        }

        /// <summary>
        ///     Фильтрация снаряжения по поисковому запросу и выбранным параметрам фильтра.
        /// </summary>
        private void UpdateFilteredEquipments()
        {
            try
            {
                var filteredByTierAndLevel = equipments.Where(equipment =>
                        (!selectedTiersSaved.Any() ||
                         selectedTiersSaved.Any(selectedTier => equipment.Tier == selectedTier.ToString())) &&
                        minUnlockLevelSaved <= equipment.UnlockLevel && maxUnlockLevelSaved >= equipment.UnlockLevel)
                    .ToList();

                var finalFiltered = string.IsNullOrWhiteSpace(SearchQuery)
                    ? filteredByTierAndLevel
                    : filteredByTierAndLevel.Where(equipment =>
                        equipment.Title.ToLowerInvariant().Contains(SearchQuery.ToLowerInvariant())).ToList();

                Equipments = new ObservableCollection<Equipment>(finalFiltered);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время обновления отфильтрованного списка снаряжения.");
                throw;
            }
        }
    }
}