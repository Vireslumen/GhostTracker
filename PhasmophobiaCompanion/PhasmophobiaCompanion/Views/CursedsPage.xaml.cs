using System;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.ViewModels;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CursedsPage : ContentPage
    {
        public CursedsPage()
        {
            try
            {
                InitializeComponent();
                var viewModel = new CursedViewModel();
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации CursedsPage.");
                throw;
            }
        }

        /// <summary>
        ///     Переход на подробную страницу проклятого предмета по нажатию на него.
        /// </summary>
        private void OnCursedTapped(object sender, EventArgs e)
        {
            try
            {
                if (sender is View view && view.BindingContext is CursedPossession selectedCursed)
                {
                    var detailPage = new CursedDetailPage(selectedCursed);
                    Application.Current.MainPage.Navigation.PushAsync(detailPage);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время перехода на подробную страницу проклятого предмета.");
                throw;
            }
        }

        /// <summary>
        ///     Выполняет поиск после его завершения.
        /// </summary>
        private void OnSearchCompleted(object sender, EventArgs e)
        {
            try
            {
                if (BindingContext is CursedViewModel viewModel) viewModel.SearchCommand.Execute(null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время поиска на странице CursedsPage.");
                throw;
            }
        }
    }
}