using System;
using System.ComponentModel;
using System.Linq;
using PhasmophobiaCompanion.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class FilterEquipmentPage : PopupPage
    {
        /// <summary>
        ///     Включена ли обработка выбора элемента списка тиров.
        /// </summary>
        private bool isSelectionHandlingEnabled = true;

        public FilterEquipmentPage(EquipmentsViewModel viewModel)
        {
            try
            {
                InitializeComponent();
                tiersCollectionView.SelectionChanged += OnItemSelected;
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации FilterEquipmentPage.");
                throw;
            }
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                if (BindingContext is EquipmentsViewModel viewModel)
                {
                    // Отключение обработки выбора элементов
                    isSelectionHandlingEnabled = false;

                    // Очистка выбранных элементы
                    tiersCollectionView.SelectedItems.Clear();

                    // Выбор элементов в соответствии с SelectedTiers
                    foreach (var selectedTier in viewModel.SelectedTiers)
                    {
                        var tier = viewModel.AllTiers.FirstOrDefault(c => c == selectedTier);
                        if (!string.IsNullOrEmpty(tier)) tiersCollectionView.SelectedItems.Add(tier);
                    }

                    // Включение обработки выбора элементов
                    isSelectionHandlingEnabled = true;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации элементов фильтра снаряжения FilterEquipmentPage.");
                throw;
            }
        }

        /// <summary>
        ///     Применение выбранных фильтров для страницы.
        /// </summary>
        private async void OnApplyFiltersClicked(object sender, EventArgs e)
        {
            try
            {
                if (BindingContext is EquipmentsViewModel viewModel)
                {
                    viewModel.MinUnlockLevel = double.TryParse(minUnlockLevel.Text, out var min) ? min : 0;
                    viewModel.MaxUnlockLevel = double.TryParse(maxUnlockLevel.Text, out var max) ? max : 100;

                    viewModel.Filter();
                    await PopupNavigation.Instance.PopAsync();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время принятия фильтрации снаряжения FilterEquipmentPage.");
                throw;
            }
        }

        /// <summary>
        ///     Изменение списка выбранных тиров во ViewModel.
        ///     Происходит при нажатии на тир в списке отображаемом в интерфейсе.
        /// </summary>
        private void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (BindingContext is EquipmentsViewModel viewModel)
                    if (isSelectionHandlingEnabled)
                    {
                        viewModel.SelectedTiers.Clear();
                        foreach (string item in e.CurrentSelection) viewModel.SelectedTiers.Add(item);
                    }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время изменения списка выбранных тиров в фильтре снаряжения FilterEquipmentPage.");
                throw;
            }
        }
    }
}