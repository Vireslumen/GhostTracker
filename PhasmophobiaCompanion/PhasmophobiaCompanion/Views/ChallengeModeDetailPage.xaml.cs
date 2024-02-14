using PhasmophobiaCompanion.Models;
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
                BindingContext = challengeMode;
            }
            catch (System.Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации ChallengeModeDetailPage.");
                throw;
            }
        }
    }
}