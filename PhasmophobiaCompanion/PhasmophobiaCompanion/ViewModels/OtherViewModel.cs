using System;
using System.Windows.Input;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Views;
using Serilog;
using Xamarin.Forms;

namespace PhasmophobiaCompanion.ViewModels
{
    /// <summary>
    ///     ViewModel для подробной некатегоризованной страницы.
    /// </summary>
    public class OtherViewModel : UnfoldingItemsViewModel
    {
        private OtherInfo otherInfo;

        public OtherViewModel(OtherInfo otherInfo)
        {
            OtherInfo = otherInfo;
            foreach (var item in OtherInfo.UnfoldingItems) item.IsExpanded = true;
            foreach (var item in OtherInfo.ExpandFieldsWithImages) item.IsExpanded = true;
            ImageTappedCommand = new Command<ImageWithDescription>(OpenImagePage);
        }

        public ICommand ImageTappedCommand { get; protected set; }
        public OtherInfo OtherInfo
        {
            get => otherInfo;
            set
            {
                otherInfo = value;
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