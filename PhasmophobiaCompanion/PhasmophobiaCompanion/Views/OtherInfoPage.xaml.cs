using System;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.ViewModels;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OtherInfoPage : ContentPage
    {
        public OtherInfoPage(OtherInfo otherInfo)
        {
            try
            {
                InitializeComponent();
                var viewModel = new OtherViewModel(otherInfo);
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации OtherInfoPage.");
                throw;
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            var viewModel = BindingContext as OtherViewModel;
            viewModel?.Cleanup();
        }
    }
}