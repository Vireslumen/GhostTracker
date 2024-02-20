using System;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Services;
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

        public EquipmentDetailViewModel(Equipment equipment)
        {
            try
            {
                dataService = DependencyService.Get<DataService>();
                EquipmentCommon = dataService.GetEquipmentCommon();
                Equipment = equipment;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации EquipmentDetailViewModel.");
                throw;
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