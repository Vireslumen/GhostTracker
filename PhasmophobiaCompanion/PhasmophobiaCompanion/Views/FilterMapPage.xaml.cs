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
    public partial class FilterMapPage : PopupPage
    {
        /// <summary>
        ///     Включена ли обработка выбора элемента списка тиров.
        /// </summary>
        private bool isSelectionHandlingEnabled = true;

        public FilterMapPage(MapsViewModel viewModel)
        {
            try
            {
                InitializeComponent();
                sizesCollectionView.SelectionChanged += OnItemSelected;
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации FilterMapPage.");
                throw;
            }
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                if (BindingContext is MapsViewModel viewModel)
                {
                    // Отключение обработки выбора элементов
                    isSelectionHandlingEnabled = false;

                    // Очистка выбранных элементы
                    sizesCollectionView.SelectedItems.Clear();

                    // Выбор элементов в соответствии с SelectedSizes
                    foreach (var selectedTier in viewModel.SelectedSizes)
                    {
                        var tier = viewModel.AllSizes.FirstOrDefault(c => c == selectedTier);
                        if (!string.IsNullOrEmpty(tier)) sizesCollectionView.SelectedItems.Add(tier);
                    }

                    // Включение обработки выбора элементов
                    isSelectionHandlingEnabled = true;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации элементов фильтра карт FilterMapPage.");
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
                if (BindingContext is MapsViewModel viewModel)
                {
                    viewModel.MinRoom = double.TryParse(minRoom.Text, out var min) ? min : 0;
                    viewModel.MaxRoom = double.TryParse(maxRoom.Text, out var max) ? max : 100;

                    viewModel.Filter();
                    await PopupNavigation.Instance.PopAsync();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время принятия фильтрации карт FilterMapPage.");
                throw;
            }
        }

        /// <summary>
        ///     Изменение списка выбранных размеров карт во ViewModel.
        ///     Происходит при нажатии на размер карты в списке отображаемом в интерфейсе.
        /// </summary>
        private void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (BindingContext is MapsViewModel viewModel)
                    if (isSelectionHandlingEnabled)
                    {
                        viewModel.SelectedSizes.Clear();
                        foreach (string item in e.CurrentSelection) viewModel.SelectedSizes.Add(item);
                    }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время изменения списка выбранных размеров карт в фильтре карт FilterMapPage.");
                throw;
            }
        }
    }
}