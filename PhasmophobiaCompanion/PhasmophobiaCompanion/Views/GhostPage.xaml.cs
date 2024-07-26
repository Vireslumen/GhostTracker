using System;
using PhasmophobiaCompanion.ViewModels;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GhostPage : ContentPage
    {
        public GhostPage()
        {
            try
            {
                InitializeComponent();
                var viewModel = new GhostsViewModel();
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации GhostPage.");
            }
        }
    }
}