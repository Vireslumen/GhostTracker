using System;
using GhostTracker.ViewModels;
using Serilog;
using Xamarin.Forms.Xaml;

namespace GhostTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CursedsPage
    {
        public CursedsPage()
        {
            try
            {
                InitializeComponent();
                var viewModel = new CursedViewModel();
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации CursedsPage.");
            }
        }
    }
}