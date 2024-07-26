using System;
using Rg.Plugins.Popup.Pages;
using Serilog;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPopup : PopupPage
    {
        public LoadingPopup()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации страницы загрузки.");
            }
        }
    }
}