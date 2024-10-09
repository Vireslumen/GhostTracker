using System;
using GhostTracker.Models;
using GhostTracker.ViewModels;
using Serilog;
using Xamarin.Forms.Xaml;

namespace GhostTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DifficultyDetailPage
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
            }
        }
    }
}