using System;
using System.Collections.Generic;
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
    ///     ViewModel для страницы списка снаряжения, поддерживает поиск и фильтрацию.
    /// </summary>
    public class EquipmentsViewModel : SearchableViewModel, IFilterable
    {
        private const int MaxUnlockLevelDefault = 100;
        private const int MinUnlockLevelDefault = 0;
        private readonly DataService dataService;
        private readonly List<Equipment> equipments;
        private EquipmentCommon equipmentCommon;
        private int maxUnlockLevelSaved;
        private int minUnlockLevelSaved;
        private List<object> selectedTiersSaved;
        private List<string> allTiers;
        private ObservableCollection<Equipment> filteredEquipments;
        private ObservableCollection<object> selectedTiers;
        private string maxUnlockLevel;
        private string minUnlockLevel;

        public EquipmentsViewModel()
        {
            try
            {
                dataService = DependencyService.Get<DataService>();
                //Загрузка данных для интерфейса.
                EquipmentCommon = dataService.GetEquipmentCommon();
                //Загрузка всего снаряжения.
                equipments = dataService.GetEquipments().OrderBy(e => e.Title).ToList();
                allTiers = dataService.GetTiers();
                AllTiers = new List<string>(allTiers);
                Equipments = new ObservableCollection<Equipment>(equipments);
                //Инициализация элементов фильтра
                SelectedTiers = new ObservableCollection<object>();
                selectedTiersSaved = new List<object>();
                maxUnlockLevelSaved = MaxUnlockLevelDefault;
                minUnlockLevelSaved = MinUnlockLevelDefault;
                maxUnlockLevel = MaxUnlockLevelDefault.ToString();
                minUnlockLevel = MinUnlockLevelDefault.ToString();
                // Инициализация команд
                EquipmentSelectedCommand = new Command<Equipment>(OnEquipmentSelected);
                FilterCommand = new Command(OnFilterTapped);
                FilterApplyCommand = new Command(OnFilterApplyTapped);
                FilterClearCommand = new Command(OnFilterClearTapped);
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
        public ICommand FilterClearCommand { get; private set; }
        public ICommand FilterCommand { get; private set; }
        /// <summary>
        ///     Коллекция всех доступных тиров оборудования.
        /// </summary>
        public List<string> AllTiers
        {
            get => allTiers;
            set => SetProperty(ref allTiers, value);
        }
        /// <summary>
        ///     Коллекция отображаемого снаряжения, которая может быть отфильтрована.
        /// </summary>
        public ObservableCollection<Equipment> Equipments
        {
            get => filteredEquipments;
            set => SetProperty(ref filteredEquipments, value);
        }
        /// <summary>
        ///     Коллекция выбранных тиров для фильтрации.
        /// </summary>
        public ObservableCollection<object> SelectedTiers
        {
            get => selectedTiers;
            set => SetProperty(ref selectedTiers, value);
        }
        public string FilterColor
        {
            get
            {
                var currentTheme = Application.Current.RequestedTheme;
                return SelectedTiers.Any() || minUnlockLevelSaved != MinUnlockLevelDefault ||
                       maxUnlockLevelSaved != MaxUnlockLevelDefault
                    ? currentTheme == OSAppTheme.Dark ? "#FD7E14" : "#FD7E14"
                    : "Transparent";
            }
        }
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
                if (isNavigating) return;
                if (selectedEquipment != null)
                {
                    // Логика для открытия страницы деталей снаряжения
                    var detailPage = new EquipmentDetailPage(selectedEquipment);
                    await NavigateWithLoadingAsync(detailPage);
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
                selectedTiersSaved = new List<object>(SelectedTiers);
                minUnlockLevelSaved = int.TryParse(minUnlockLevel, out var max) ? max : MaxUnlockLevelDefault;
                maxUnlockLevelSaved = int.TryParse(maxUnlockLevel, out var min) ? min : MinUnlockLevelDefault;
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
        ///     Сброс параметров фильтрации и закрытие окна фильтрации.
        /// </summary>
        private void OnFilterClearTapped()
        {
            try
            {
                selectedTiersSaved = new List<object>();
                SelectedTiers = new ObservableCollection<object>();
                maxUnlockLevelSaved = MaxUnlockLevelDefault;
                minUnlockLevelSaved = MinUnlockLevelDefault;
                MaxUnlockLevel = MaxUnlockLevelDefault.ToString();
                MinUnlockLevel = MinUnlockLevelDefault.ToString();
                Filter();
                OnPropertyChanged(nameof(FilterColor));
                PopupNavigation.Instance.PopAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при сбросе фильтрации.");
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
        protected override void PerformSearch()
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

                var finalFiltered = string.IsNullOrWhiteSpace(SearchText)
                    ? filteredByTierAndLevel
                    : filteredByTierAndLevel.Where(equipment =>
                        equipment.Title.ToLowerInvariant().Contains(SearchText.ToLowerInvariant())).ToList();

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