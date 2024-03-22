using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class EquipmentDetailViewModel : BaseViewModel
    {
        private readonly DataService dataService;
        private Equipment equipment;
        private EquipmentCommon equipmentCommon;
        private List<Equipment> equipmentsSameTypeCollection;
        public ICommand EquipmentSelectedCommand { get; private set; }

        public EquipmentDetailViewModel(Equipment equipment)
        {
            try
            {
                dataService = DependencyService.Get<DataService>();
                EquipmentCommon = dataService.GetEquipmentCommon();
                EquipmentsSameTypeCollection = dataService.GetEquipmentsSameTypeCollection(equipment);
                Equipment = equipment;
                EquipmentSelectedCommand = new Command<Equipment>(OnEquipmentSelected);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации EquipmentDetailViewModel.");
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
                    "Ошибка во время перехода на подробную страницу снаряжения из другой подробной страницы снаряжения.");
                throw;
            }
        }
        public List<Equipment> EquipmentsSameTypeCollection
        {
            get => equipmentsSameTypeCollection;
            set
            {
                equipmentsSameTypeCollection = value;
                OnPropertyChanged();
            }
        }

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
    }
}