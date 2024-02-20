using System;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Services;
using Serilog;
using Xamarin.Forms;

namespace PhasmophobiaCompanion.ViewModels
{
    /// <summary>
    ///     ViewModel для подробной страницы карты.
    /// </summary>
    public class MapDetailViewModel : BaseViewModel
    {
        private readonly DataService dataService;
        private Map map;
        private MapCommon mapCommon;

        public MapDetailViewModel(Map map)
        {
            try
            {
                dataService = DependencyService.Get<DataService>();
                MapCommon = dataService.GetMapCommon();
                Map = map;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации MapDetailViewModel.");
                throw;
            }
        }

        public Map Map
        {
            get => map;
            set
            {
                map = value;
                OnPropertyChanged();
            }
        }
        public MapCommon MapCommon
        {
            get => mapCommon;
            set
            {
                mapCommon = value;
                OnPropertyChanged();
            }
        }
    }
}