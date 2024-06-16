using System;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.ViewModels;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChallengeModeDetailPage : ContentPage
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
                throw;
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