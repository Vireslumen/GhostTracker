using System;
using GhostTracker.Models;
using GhostTracker.ViewModels;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GhostTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EquipmentDetailPage : ContentPage
    {
        public EquipmentDetailPage(Equipment equipment)
        {
            try
            {
                InitializeComponent();
                var viewModel = new EquipmentDetailViewModel(equipment);
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации EquipmentDetailPage.");
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            var viewModel = BindingContext as EquipmentDetailViewModel;
            viewModel?.Cleanup();
        }
    }
}