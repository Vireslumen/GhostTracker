using System;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapsPage : ContentPage
    {
        public MapsPage()
        {
            InitializeComponent();
            var viewModel = new MapsViewModel();
            BindingContext = viewModel;
        }

        /// <summary>
        ///     Открывает всплывающее окно для фильтрации снаряжения.
        /// </summary>
        private async void FilterTapped(object sender, EventArgs e)
        {
            if (BindingContext is MapsViewModel viewModel)
            {
                var filterPage = new FilterMapPage(viewModel);
                await PopupNavigation.Instance.PushAsync(filterPage);
            }
        }

        /// <summary>
        ///     Переход на подробную страницу карты по нажатию на неё.
        /// </summary>
        private void OnMapTapped(object sender, EventArgs e)
        {
            if (sender is View view && view.BindingContext is Map selectMap)
            {
                var detailPage = new MapDetailPage(selectMap);
                Application.Current.MainPage.Navigation.PushAsync(detailPage);
            }
        }

        /// <summary>
        ///     Выполняет поиск после его завершения.
        /// </summary>
        private void OnSearchCompleted(object sender, EventArgs e)
        {
            if (BindingContext is MapsViewModel viewModel) viewModel.SearchCommand.Execute(null);
        }
    }
}