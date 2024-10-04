using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GhostTracker.Models;
using GhostTracker.Services;
using GhostTracker.Views;
using Serilog;
using Xamarin.Forms;

namespace GhostTracker.ViewModels
{
    /// <summary>
    ///     ViewModel для подробной страницы снаряжения.
    /// </summary>
    public class EquipmentDetailViewModel : UnfoldingItemsViewModel
    {
        private Equipment equipment;
        private EquipmentCommon equipmentCommon;
        private List<Equipment> equipmentsSameTypeCollection;

        public EquipmentDetailViewModel(Equipment equipment)
        {
            var dataService = DependencyService.Get<DataService>();
            EquipmentCommon = dataService.GetEquipmentCommon();
            EquipmentsSameTypeCollection = dataService.GetEquipmentsSameTypeCollection(equipment);
            Equipment = equipment;
            foreach (var item in Equipment.UnfoldingItems) item.IsExpanded = true;
            Equipment.EquipmentRelatedClues = new List<Clue>
            (dataService.GetClues().Where(c => Equipment.CluesId.Contains(c.Id))
                .ToList());
            if (Equipment.EquipmentRelatedClues.Count > 0) IsRelatedCluesExist = true;
            EquipmentSelectedCommand = new Command<Equipment>(OnEquipmentSelected);
            ClueSelectedCommand = new Command<Clue>(OpenCluePage);
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

        public void Cleanup()
        {
            ToggleExpandCommand = null;
            ClueSelectedCommand = null;
            EquipmentSelectedCommand = null;
        }

        /// <summary>
        ///     Переход на подробную страницу выбранного снаряжения.
        /// </summary>
        /// <param name="selectedEquipment">Выбранное снаряжение.</param>
        private async void OnEquipmentSelected(Equipment selectedEquipment)
        {
            try
            {
                if (IsNavigating) return;
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
            }
        }

        /// <summary>
        ///     Переход на страницу связанного со снаряжением улики при нажатии на нее.
        /// </summary>
        private async void OpenCluePage(Clue clueItem)
        {
            try
            {
                if (IsNavigating) return;
                var page = new ClueDetailPage(clueItem);
                await NavigateWithLoadingAsync(page);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время перехода на страницу улики с подробной страницы снаряжения.");
            }
        }
    }
}