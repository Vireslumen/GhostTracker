using System;
using GhostTracker.ViewModels;
using Serilog;
using Xamarin.Forms.Xaml;

namespace GhostTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChallengeModesPage
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
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            var viewModel = BindingContext as ChallengeModeViewModel;
            viewModel?.Cleanup();
        }
    }
}