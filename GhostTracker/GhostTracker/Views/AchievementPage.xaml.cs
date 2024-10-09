using System;
using GhostTracker.ViewModels;
using Serilog;
using Xamarin.Forms.Xaml;

namespace GhostTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AchievementPage
    {
        public AchievementPage()
        {
            try
            {
                InitializeComponent();
                var viewModel = new AchievementsViewModel();
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации AchievementPage.");
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            var viewModel = BindingContext as AchievementsViewModel;
            viewModel?.Cleanup();
        }
    }
}