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
    public partial class EquipmentPage : ContentPage
    {
        public EquipmentsViewModel viewModel;

        public EquipmentPage()
        {
            try
            {
                InitializeComponent();
                viewModel = new EquipmentsViewModel();
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации EquipmentPage.");
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
                if (BindingContext is EquipmentsViewModel viewModel)
                {
                    var filterPage = new FilterEquipmentPage(viewModel);
                    await PopupNavigation.Instance.PushAsync(filterPage);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время открытия фильтра для страницы EquipmentPage.");
                throw;
            }
        }

        /// <summary>
        ///     Переход на подробную страницу снаряжению по нажатию на него.
        /// </summary>
        private void OnEquipmentTapped(object sender, EventArgs e)
        {
            try
            {
                if (sender is View view && view.BindingContext is Equipment selectedEquipment)
                {
                    viewModel.SelectedEquipment = selectedEquipment;
                    var detailPage = new EquipmentDetailPage(viewModel);
                    Application.Current.MainPage.Navigation.PushAsync(detailPage);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время перехода на подробную страницу снаряжения EquipmentDetailPage.");
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
                if (BindingContext is EquipmentsViewModel viewModel) viewModel.SearchCommand.Execute(null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время поиска на странице EquipmentPage.");
                throw;
            }
        }
    }
}