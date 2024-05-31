using System;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.ViewModels;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClueDetailPage : ContentPage
    {
        public ClueDetailPage(Clue clue)
        {
            try
            {
                InitializeComponent();
                var viewModel = new ClueDetailViewModel(clue);
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации ClueDetailPage.");
                throw;
            }
        }
    }
}