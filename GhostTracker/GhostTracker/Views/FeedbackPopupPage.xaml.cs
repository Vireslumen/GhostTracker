using System;
using GhostTracker.ViewModels;
using Rg.Plugins.Popup.Pages;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GhostTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedbackPopupPage : PopupPage
    {
        public FeedbackPopupPage()
        {
            try
            {
                InitializeComponent();
                BindingContext = new FeedbackViewModel();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при инициализации страницы фидбэка.");
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            // Сброс флага в AppShell
            ((AppShell) Shell.Current).IsFeedbackPopupOpen = false;
            // Отписка от событий
            var viewModel = BindingContext as GhostDetailViewModel;
            viewModel?.Cleanup();
        }
    }
}