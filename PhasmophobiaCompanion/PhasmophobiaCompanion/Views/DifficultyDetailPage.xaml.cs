using PhasmophobiaCompanion.Models;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DifficultyDetailPage : ContentPage
    {
        public DifficultyDetailPage(Difficulty difficulty)
        {
            try
            {
                InitializeComponent();
                BindingContext = difficulty;
            }
            catch (System.Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации DifficultyDetailPage.");
                throw;
            }
        }
    }
}