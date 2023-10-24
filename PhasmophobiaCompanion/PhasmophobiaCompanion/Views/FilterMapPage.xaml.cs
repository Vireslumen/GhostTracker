using PhasmophobiaCompanion.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class FilterMapPage : PopupPage
    {
        private bool isSelectionHandlingEnabled = true;
        public FilterMapPage(MapsViewModel viewModel)
        {
            InitializeComponent();
            sizesCollectionView.SelectionChanged += OnItemSelected;
            BindingContext = viewModel;

        }
        private void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            if (BindingContext is MapsViewModel viewModel)
            {
                if (isSelectionHandlingEnabled)
                {
                    viewModel.SelectedSizes.Clear();
                    foreach (string item in e.CurrentSelection)
                    {
                        viewModel.SelectedSizes.Add(item);
                    }
                }
            }

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is MapsViewModel viewModel)
            {
                // Отключите обработку выбора элементов
                isSelectionHandlingEnabled = false;

                // Очистите выбранные элементы
                sizesCollectionView.SelectedItems.Clear();

                // Выберите элементы в соответствии с SelectedClues
                foreach (var selectedTier in viewModel.SelectedSizes)
                {

                    var tier = viewModel.AllSizes.FirstOrDefault(c => c == selectedTier);
                    if (!string.IsNullOrEmpty(tier))
                    {
                        sizesCollectionView.SelectedItems.Add(tier);
                    }
                }

                // Включите обработку выбора элементов
                isSelectionHandlingEnabled = true;
            }
        }
        private async void OnApplyFiltersClicked(object sender, EventArgs e)
        {
            if (BindingContext is MapsViewModel viewModel)
            {
                viewModel.MinRoom = double.TryParse(minRoom.Text, out var min) ? min : 0;
                viewModel.MaxRoom = double.TryParse(maxRoom.Text, out var max) ? max : 100;

                viewModel.Filter();
                await PopupNavigation.Instance.PopAsync();
            }
        }
    }
}