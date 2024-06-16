using System;
using PhasmophobiaCompanion.ViewModels;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            try
            {
                InitializeComponent();
                var viewModel = new SettingsViewModel();
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации страницы настроек.");
                throw;
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            var viewModel = BindingContext as SettingsViewModel;
            viewModel?.Cleanup();
        }
    }
}