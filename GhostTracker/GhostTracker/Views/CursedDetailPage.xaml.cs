using System;
using GhostTracker.Models;
using GhostTracker.ViewModels;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GhostTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CursedDetailPage : ContentPage
    {
        public CursedDetailPage(CursedPossession cursed)
        {
            try
            {
                InitializeComponent();
                var viewModel = new CursedDetailViewModel(cursed);
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации CursedDetailPage.");
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            var viewModel = BindingContext as CursedDetailViewModel;
            viewModel?.Cleanup();
        }
    }
}