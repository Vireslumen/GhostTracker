using System;
using System.ComponentModel;
using GhostTracker.ViewModels;
using Serilog;
using Xamarin.Forms.Xaml;

namespace GhostTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class PatchAlertPage
    {
        public PatchAlertPage(MainPageViewModel viewModel)
        {
            try
            {
                InitializeComponent();
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при инициализации страницы PopupPage");
            }
        }
    }
}