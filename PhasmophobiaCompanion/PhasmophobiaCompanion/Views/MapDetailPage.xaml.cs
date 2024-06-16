using System;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.ViewModels;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapDetailPage : ContentPage
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
                throw;
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