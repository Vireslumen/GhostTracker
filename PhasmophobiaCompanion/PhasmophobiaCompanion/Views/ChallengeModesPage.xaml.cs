using System;
using PhasmophobiaCompanion.ViewModels;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChallengeModesPage : ContentPage
    {
        public ChallengeModesPage()
        {
            try
            {
                InitializeComponent();
                var viewModel = new ChallengeModeViewModel();
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации ChallengeModesPage.");
                throw;
            }
        }
    }
}