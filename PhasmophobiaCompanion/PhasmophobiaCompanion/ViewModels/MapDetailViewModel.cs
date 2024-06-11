using System;
using System.Windows.Input;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Services;
using PhasmophobiaCompanion.Views;
using Serilog;
using Xamarin.Forms;

namespace PhasmophobiaCompanion.ViewModels
{
    /// <summary>
    ///     ViewModel для подробной страницы карты.
    /// </summary>
    public class MapDetailViewModel : UnfoldingItemsViewModel
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
                foreach (var item in Map.UnfoldingItems) item.IsExpanded = true;
                foreach (var item in Map.ExpandFieldsWithImages) item.IsExpanded = true;
                ImageTappedCommand = new Command<ImageWithDescription>(OpenImagePage);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации MapDetailViewModel.");
                throw;
            }
        }

        public ICommand ImageTappedCommand { get; protected set; }
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

        private async void OpenImagePage(ImageWithDescription image)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new ImagePage(image));
        }
    }
}