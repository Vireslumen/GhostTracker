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
    ///     ViewModel для страницы списка снаряжения, поддерживает поиск и фильтрацию.
    /// </summary>
    public class EquipmentsViewModel : BaseViewModel, ISearchable, IFilterable
    {
        private readonly DataService dataService;
        private readonly ObservableCollection<Equipment> equipments;
        private double maxUnlockLevel = 100;
        private double minUnlockLevel;
        private EquipmentCommon equipmentCommon;
        private ObservableCollection<Equipment> filteredEquipments;
        private ObservableCollection<string> allTiers;
        private ObservableCollection<string> selectedTiers;
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
                SelectedTiers = new ObservableCollection<string>();
                // Инициализация команд
                SearchCommand = new Command<string>(OnSearchCompleted);
                EquipmentSelectedCommand = new Command<Equipment>(OnEquipmentSelected);
                FilterCommand = new Command(OnFilterTapped);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации EquipmentsViewModel.");
                throw;
            }
        }

        public double MaxUnlockLevel
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
        public double MinUnlockLevel
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
        public ICommand EquipmentSelectedCommand { get; private set; }
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
        public ObservableCollection<string> SelectedTiers
        {
            get => selectedTiers;
            set => SetProperty(ref selectedTiers, value);
        }

        /// <summary>
        ///     Фильтрация оборудования по выбранным тирам и диапазону уровней разблокировки.
        /// </summary>
        public void Filter()
        {
            try
            {
                //Фильтрация по выбранным тирам.
                var filteredTier = equipments.Where(equipment =>
                    !SelectedTiers.Any() || SelectedTiers.Any(selectedTier => equipment.Tier == selectedTier)).ToList();
                //Фильтрация по диапазону уровней разблокировки.
                var filteredLevel = filteredTier.Where(equipment =>
                    MinUnlockLevel <= equipment.UnlockLevel && MaxUnlockLevel >= equipment.UnlockLevel).ToList();

                Equipments = new ObservableCollection<Equipment>(filteredLevel);
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
                if (string.IsNullOrWhiteSpace(SearchQuery))
                {
                    Equipments = new ObservableCollection<Equipment>(equipments);
                }
                else
                {
                    var filtered = equipments.Where(equipment =>
                        equipment.Title.ToLowerInvariant().Contains(SearchQuery.ToLowerInvariant())).ToList();
                    Equipments = new ObservableCollection<Equipment>(filtered);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время поиска снаряжения.");
                throw;
            }
        }
    }
}