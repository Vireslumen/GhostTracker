using System;
using GhostTracker.ViewModels;
using Serilog;
using Xamarin.Forms.Xaml;

namespace GhostTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GhostPage
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