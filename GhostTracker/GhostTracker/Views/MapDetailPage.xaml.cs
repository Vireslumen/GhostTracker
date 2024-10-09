using System;
using GhostTracker.Models;
using GhostTracker.ViewModels;
using Serilog;
using Xamarin.Forms.Xaml;

namespace GhostTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapDetailPage
    {
        public MapDetailPage(Map map)
        {
            try
            {
                InitializeComponent();
                var viewModel = new MapDetailViewModel(map);
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации MapDetailPage.");
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            var viewModel = BindingContext as MapDetailViewModel;
            viewModel?.Cleanup();
        }
    }
}