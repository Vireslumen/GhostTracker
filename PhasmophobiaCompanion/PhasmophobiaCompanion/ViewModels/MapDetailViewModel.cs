using System;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class MapDetailViewModel : BaseViewModel
    {
        private readonly DataService dataService;
        private Map map;
        private MapCommon mapCommon;
        public ICommand ImageTappedCommand;
        public MapDetailViewModel(Map map)
        {
            try
            {
                dataService = DependencyService.Get<DataService>();
                MapCommon = dataService.GetMapCommon();
                Map = map;
                ImageTappedCommand = new Command<ImageWithDescription>(OpenImagePage);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации MapDetailViewModel.");
                throw;
            }
        }
        private async void OpenImagePage(ImageWithDescription image)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new ImagePage(image));
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