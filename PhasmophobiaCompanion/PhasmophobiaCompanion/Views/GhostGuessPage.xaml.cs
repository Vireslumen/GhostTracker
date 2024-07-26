using System;
using PhasmophobiaCompanion.ViewModels;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GhostGuessPage : ContentPage
    {
        public GhostGuessPage()
        {
            try
            {
                InitializeComponent();
                var viewModel = new GhostGuessViewModel();
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при инициализации страницы определения призрака.");
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            var viewModel = BindingContext as GhostGuessViewModel;
            viewModel?.Cleanup();
        }
    }
}