using System;
using PhasmophobiaCompanion.ViewModels;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AchievementPage : ContentPage
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
                throw;
            }
        }
    }
}