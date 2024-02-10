using PhasmophobiaCompanion.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChallengeModeDetailPage : ContentPage
    {
        public ChallengeModeDetailPage(ChallengeMode challengeMode)
        {
            InitializeComponent();
            BindingContext = challengeMode;
        }
    }
}