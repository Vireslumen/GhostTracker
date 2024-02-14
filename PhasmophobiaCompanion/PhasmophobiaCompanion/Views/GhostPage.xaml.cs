using System;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.ViewModels;
using Rg.Plugins.Popup.Services;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GhostPage : ContentPage
    {
        public GhostPage()
        {
            try
            {
                InitializeComponent();
                var viewModel = new GhostsViewModel();
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации GhostPage.");
                throw;
            }
        }

        /// <summary>
        ///     Открывает всплывающее окно для фильтрации призраков.
        /// </summary>
        private async void FilterTapped(object sender, EventArgs e)
        {
            try
            {
                if (BindingContext is GhostsViewModel viewModel)
                {
                    var filterPage = new FilterPage(viewModel);
                    await PopupNavigation.Instance.PushAsync(filterPage);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время открытия фильтра на странице призраков GhostPage.");
                throw;
            }
        }

        /// <summary>
        ///     Переход на подробную страницу призрака по нажатию на него.
        /// </summary>
        private void OnGhostTapped(object sender, EventArgs e)
        {
            try
            {
                if (sender is View view && view.BindingContext is Ghost selectedGhost)
                {
                    var detailPage = new GhostDetailPage(selectedGhost);
                    Application.Current.MainPage.Navigation.PushAsync(detailPage);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время перехода на подробную страницу призрака GhostDetailPage из страницы призраков GhostPage.");
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
                if (BindingContext is GhostsViewModel viewModel) viewModel.SearchCommand.Execute(null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время поиска на странице призраков GhostPage.");
                throw;
            }
        }
    }
}