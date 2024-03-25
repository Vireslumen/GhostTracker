using System;
using System.ComponentModel;
using PhasmophobiaCompanion.ViewModels;
using Rg.Plugins.Popup.Pages;
using Serilog;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class PatchAlertPage : PopupPage
    {
        public PatchAlertPage(MainPageViewModel viewModel)
        {
            try
            {
                InitializeComponent();
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при инициализации страницы PopupPage");
                throw;
            }
        }
    }
}