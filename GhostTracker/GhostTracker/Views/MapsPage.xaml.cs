using System;
using GhostTracker.ViewModels;
using Serilog;
using Xamarin.Forms.Xaml;

namespace GhostTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapsPage
    {
        public MapsPage()
        {
            try
            {
                InitializeComponent();
                var viewModel = new MapsViewModel();
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации MapsPage.");
            }
        }
    }
}