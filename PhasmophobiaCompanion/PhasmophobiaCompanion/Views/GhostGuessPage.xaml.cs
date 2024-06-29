using PhasmophobiaCompanion.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GhostGuessPage : ContentPage
    {
        public GhostGuessPage()
        {
            InitializeComponent();
            var viewModel = new GhostGuessViewModel();
            BindingContext = viewModel;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            var viewModel = BindingContext as GhostGuessViewModel;
            viewModel?.Cleanup();
        }
    }
}