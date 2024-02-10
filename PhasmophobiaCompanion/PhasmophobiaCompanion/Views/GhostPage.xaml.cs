using System;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GhostPage : ContentPage
    {
        public GhostPage()
        {
            InitializeComponent();
            var viewModel = new GhostsViewModel();
            BindingContext = viewModel;
        }

        /// <summary>
        ///     Открывает всплывающее окно для фильтрации снаряжения.
        /// </summary>
        private async void FilterTapped(object sender, EventArgs e)
        {
            if (BindingContext is GhostsViewModel viewModel)
            {
                var filterPage = new FilterPage(viewModel);
                await PopupNavigation.Instance.PushAsync(filterPage);
            }
        }

        /// <summary>
        ///     Переход на подробную страницу призрака по нажатию на него.
        /// </summary>
        private void OnGhostTapped(object sender, EventArgs e)
        {
            if (sender is View view && view.BindingContext is Ghost selectedGhost)
            {
                var detailPage = new GhostDetailPage(selectedGhost);
                Application.Current.MainPage.Navigation.PushAsync(detailPage);
            }
        }

        /// <summary>
        ///     Выполняет поиск после его завершения.
        /// </summary>
        private void OnSearchCompleted(object sender, EventArgs e)
        {
            if (BindingContext is GhostsViewModel viewModel) viewModel.SearchCommand.Execute(null);
        }
    }
}