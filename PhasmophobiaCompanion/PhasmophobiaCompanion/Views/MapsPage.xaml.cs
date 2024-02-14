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
    public partial class MapsPage : ContentPage
    {
        private MapsViewModel viewModel;
        public MapsPage()
        {
            try
            {
                InitializeComponent();
                viewModel = new MapsViewModel();
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации MapsPage.");
                throw;
            }
        }

        /// <summary>
        ///     Открывает всплывающее окно для фильтрации снаряжения.
        /// </summary>
        private async void FilterTapped(object sender, EventArgs e)
        {
            try
            {
                if (BindingContext is MapsViewModel viewModel)
                {
                    var filterPage = new FilterMapPage(viewModel);
                    await PopupNavigation.Instance.PushAsync(filterPage);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время открытия фильтра страницы карт MapsPage.");
                throw;
            }
        }

        /// <summary>
        ///     Переход на подробную страницу карты по нажатию на неё.
        /// </summary>
        private void OnMapTapped(object sender, EventArgs e)
        {
            try
            {
                if (sender is View view && view.BindingContext is Map selectMap)
                {
                    viewModel.SelectedMap = selectMap;
                    var detailPage = new MapDetailPage(viewModel);
                    Application.Current.MainPage.Navigation.PushAsync(detailPage);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время перехода на подробную страницу карты MapDetailPage.");
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
                if (BindingContext is MapsViewModel viewModel) viewModel.SearchCommand.Execute(null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время поиска на странице карт MapsPage.");
                throw;
            }
        }
    }
}