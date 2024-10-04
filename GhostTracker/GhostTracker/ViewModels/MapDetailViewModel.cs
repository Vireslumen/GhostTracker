using System;
using System.Windows.Input;
using GhostTracker.Models;
using GhostTracker.Services;
using GhostTracker.Views;
using Serilog;
using Xamarin.Forms;

namespace GhostTracker.ViewModels
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
            dataService = DependencyService.Get<DataService>();
            MapCommon = dataService.GetMapCommon();
            Map = map;
            foreach (var item in Map.UnfoldingItems) item.IsExpanded = true;
            foreach (var item in Map.ExpandFieldsWithImages) item.IsExpanded = true;
            ImageTappedCommand = new Command<ImageWithDescription>(OpenImagePage);
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

        public void Cleanup()
        {
            ToggleExpandCommand = null;
            ImageTappedCommand = null;
        }

        private async void OpenImagePage(ImageWithDescription image)
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new ImagePage(image));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при открытии страницы изображения.");
            }
        }
    }
}