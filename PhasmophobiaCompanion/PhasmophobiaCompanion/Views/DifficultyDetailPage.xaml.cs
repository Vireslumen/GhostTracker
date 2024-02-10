using PhasmophobiaCompanion.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DifficultyDetailPage : ContentPage
    {
        public DifficultyDetailPage(Difficulty difficulty)
        {
            InitializeComponent();
            BindingContext = difficulty;
        }
    }
}