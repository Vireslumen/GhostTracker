using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Services;
using PhasmophobiaCompanion.Views;
using Serilog;
using Xamarin.Forms;

namespace PhasmophobiaCompanion.ViewModels
{
    /// <summary>
    ///     ViewModel для подробной страницы снаряжения.
    /// </summary>
    public class EquipmentDetailViewModel : UnfoldingItemsViewModel
    {
        private readonly DataService dataService;
        private Equipment equipment;
        private EquipmentCommon equipmentCommon;
        private List<Equipment> equipmentsSameTypeCollection;

        public EquipmentDetailViewModel(Equipment equipment)
        {
            try
            {
                dataService = DependencyService.Get<DataService>();
                EquipmentCommon = dataService.GetEquipmentCommon();
                EquipmentsSameTypeCollection = dataService.GetEquipmentsSameTypeCollection(equipment);
                Equipment = equipment;
                foreach (var item in Equipment.UnfoldingItems) item.IsExpanded = true;
                Equipment.EquipmentRelatedClues = new List<Clue>
                (dataService.GetClues().Where(c => Equipment.CluesID.Contains(c.ID))
                    .ToList());
                if (Equipment.EquipmentRelatedClues.Count > 0) IsRelatedCluesExist = true;
                EquipmentSelectedCommand = new Command<Equipment>(OnEquipmentSelected);
                ClueSelectedCommand = new Command<Clue>(OpenCluePage);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации EquipmentDetailViewModel.");
                throw;
            }
        }

        public bool IsRelatedCluesExist { get; set; }
        public Equipment Equipment
        {
            get => equipment;
            set
            {
                equipment = value;
                OnPropertyChanged();
            }
        }
        public EquipmentCommon EquipmentCommon
        {
            get => equipmentCommon;
            set
            {
                equipmentCommon = value;
                OnPropertyChanged();
            }
        }
        public ICommand ClueSelectedCommand { get; private set; }
        public ICommand EquipmentSelectedCommand { get; private set; }
        public List<Equipment> EquipmentsSameTypeCollection
        {
            get => equipmentsSameTypeCollection;
            set
            {
                equipmentsSameTypeCollection = value;
                OnPropertyChanged();
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
                    "Ошибка во время перехода на подробную страницу снаряжения из другой подробной страницы снаряжения.");
                throw;
            }
        }

        /// <summary>
        ///     Переход на страницу связанного со снаряжением улики при нажатии на нее.
        /// </summary>
        private async void OpenCluePage(Clue clueItem)
        {
            try
            {
                if (isNavigating) return;
                var page = new ClueDetailPage(clueItem);
                await NavigateWithLoadingAsync(page);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время перехода на страницу улики с подробной страницы снаряжения.");
                throw;
            }
        }
    }
}