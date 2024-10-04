using System;
using System.Windows.Input;
using GhostTracker.Models;
using GhostTracker.Views;
using Serilog;
using Xamarin.Forms;

namespace GhostTracker.ViewModels
{
    /// <summary>
    ///     ViewModel для подробной страницы проклятого предмета.
    /// </summary>
    public class CursedDetailViewModel : UnfoldingItemsViewModel
    {
        private CursedPossession cursedPossession;

        public CursedDetailViewModel(CursedPossession cursedPossession)
        {
            CursedPossession = cursedPossession;
            foreach (var item in CursedPossession.UnfoldingItems) item.IsExpanded = true;
            foreach (var item in CursedPossession.ExpandFieldsWithImages) item.IsExpanded = true;
            ImageTappedCommand = new Command<ImageWithDescription>(OpenImagePage);
        }

        public CursedPossession CursedPossession
        {
            get => cursedPossession;
            set
            {
                cursedPossession = value;
                OnPropertyChanged();
            }
        }
        public ICommand ImageTappedCommand { get; protected set; }

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