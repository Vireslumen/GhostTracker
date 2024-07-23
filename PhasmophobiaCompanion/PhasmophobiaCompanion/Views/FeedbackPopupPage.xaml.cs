using PhasmophobiaCompanion.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedbackPopupPage : PopupPage
    {
        public FeedbackPopupPage(string sourcePageName)
        {
            InitializeComponent();
            BindingContext = new FeedbackViewModel();
            ((FeedbackViewModel) BindingContext).SourcePage = sourcePageName;
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