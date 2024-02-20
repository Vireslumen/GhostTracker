using System;
using PhasmophobiaCompanion.ViewModels;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CursedsPage : ContentPage
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
                throw;
            }
        }
    }
}