using System;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EquipmentPage : ContentPage
    {
        public EquipmentsViewModel viewModel;

        public EquipmentPage()
        {
            InitializeComponent();
            viewModel = new EquipmentsViewModel();
            BindingContext = viewModel;
        }

        /// <summary>
        ///     Открывает всплывающее окно для фильтрации снаряжения.
        /// </summary>
        private async void FilterTapped(object sender, EventArgs e)
        {
            if (BindingContext is EquipmentsViewModel viewModel)
            {
                var filterPage = new FilterEquipmentPage(viewModel);
                await PopupNavigation.Instance.PushAsync(filterPage);
            }
        }

        /// <summary>
        ///     Переход на подробную страницу снаряжению по нажатию на него.
        /// </summary>
        private void OnEquipmentTapped(object sender, EventArgs e)
        {
            if (sender is View view && view.BindingContext is Equipment selectedEquipment)
            {
                viewModel.SelectedEquipment = selectedEquipment;
                var detailPage = new EquipmentDetailPage(viewModel);
                Application.Current.MainPage.Navigation.PushAsync(detailPage);
            }
        }

        /// <summary>
        ///     Выполняет поиск после его завершения.
        /// </summary>
        private void OnSearchCompleted(object sender, EventArgs e)
        {
            if (BindingContext is EquipmentsViewModel viewModel) viewModel.SearchCommand.Execute(null);
        }
    }
}