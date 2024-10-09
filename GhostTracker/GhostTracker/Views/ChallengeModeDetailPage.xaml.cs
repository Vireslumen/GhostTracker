using System;
using GhostTracker.Models;
using GhostTracker.ViewModels;
using Serilog;
using Xamarin.Forms.Xaml;

namespace GhostTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChallengeModeDetailPage
    {
        public ChallengeModeDetailPage(ChallengeMode challengeMode)
        {
            try
            {
                InitializeComponent();
                var viewModel = new ChallengeModeDetailViewModel(challengeMode);
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации ChallengeModeDetailPage.");
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            var viewModel = BindingContext as ChallengeModeDetailViewModel;
            viewModel?.Cleanup();
        }
    }
}