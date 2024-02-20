using System;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.ViewModels;
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
                var viewModel = new DifficultyDetailViewModel(difficulty);
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации DifficultyDetailPage.");
                throw;
            }
        }
    }
}