using System;
using System.ComponentModel;
using System.Linq;
using PhasmophobiaCompanion.Models;
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
    public partial class FilterPage : PopupPage
    {
        /// <summary>
        ///     Включена ли обработка выбора элемента списка тиров.
        /// </summary>
        private bool isSelectionHandlingEnabled = true;

        public FilterPage(GhostsViewModel viewModel)
        {
            try
            {
                InitializeComponent();
                cluesCollectionView.SelectionChanged += OnItemSelected;
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации FilterPage.");
                throw;
            }
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                if (BindingContext is GhostsViewModel viewModel)
                {
                    // Отключение обработки выбора элементов
                    isSelectionHandlingEnabled = false;

                    // Очистка выбранных элементы
                    cluesCollectionView.SelectedItems.Clear();

                    // Выбор элементов в соответствии с SelectedSizes
                    foreach (var selectedClue in viewModel.SelectedClues)
                    {
                        var clue = viewModel.AllClues.FirstOrDefault(c => c.Title == selectedClue.Title);
                        if (!string.IsNullOrEmpty(clue.Title)) cluesCollectionView.SelectedItems.Add(clue);
                    }

                    // Включение обработки выбора элементов
                    isSelectionHandlingEnabled = true;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации элементов фильтра призраков FilterPage.");
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
                if (BindingContext is GhostsViewModel viewModel)
                {
                    viewModel.Filter();
                    await PopupNavigation.Instance.PopAsync();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время принятия фильтрации призраков FilterPage.");
                throw;
            }
        }

        /// <summary>
        ///     Изменение списка выбранных улик во ViewModel.
        ///     Происходит при нажатии на улику в списке отображаемом в интерфейсе.
        /// </summary>
        private void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (BindingContext is GhostsViewModel viewModel)
                    if (isSelectionHandlingEnabled)
                    {
                        viewModel.SelectedClues.Clear();
                        foreach (Clue item in e.CurrentSelection) viewModel.SelectedClues.Add(item);
                    }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время изменения списка выбранных улик в фильтре призраков FilterPage.");
                throw;
            }
        }
    }
}