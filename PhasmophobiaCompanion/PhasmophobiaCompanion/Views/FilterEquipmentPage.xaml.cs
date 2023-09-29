using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using System.Linq;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.ViewModels;
using System;
using Rg.Plugins.Popup.Services;

namespace PhasmophobiaCompanion.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class FilterEquipmentPage: PopupPage
    {
        private bool isSelectionHandlingEnabled = true;
        public FilterEquipmentPage(EquipmentsViewModel viewModel)
		{
			InitializeComponent ();
            tiersCollectionView.SelectionChanged += OnItemSelected;
            BindingContext = viewModel;
        }
        private void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            if (BindingContext is EquipmentsViewModel viewModel)
            {
                if (isSelectionHandlingEnabled)
                {

                    viewModel.SelectedTiers.Clear();
                    foreach (string item in e.CurrentSelection)
                    {
                        viewModel.SelectedTiers.Add(item);
                    }
                }
            }

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is EquipmentsViewModel viewModel)
            {
                // Отключите обработку выбора элементов
                isSelectionHandlingEnabled = false;

                // Очистите выбранные элементы
                tiersCollectionView.SelectedItems.Clear();

                // Выберите элементы в соответствии с SelectedClues
                foreach (var selectedTier in viewModel.SelectedTiers)
                {

                    var tier = viewModel.AllTiers.FirstOrDefault(c => c == selectedTier);
                    if (!string.IsNullOrEmpty(tier))
                    {
                        tiersCollectionView.SelectedItems.Add(tier);
                    }
                }

                // Включите обработку выбора элементов
                isSelectionHandlingEnabled = true;
            }
        }
        private async void OnApplyFiltersClicked(object sender, EventArgs e)
        {
            if (BindingContext is EquipmentsViewModel viewModel)
            {
                viewModel.MinUnlockLevel = double.TryParse(minUnlockLevel.Text, out var min) ? min : 0;
                viewModel.MaxUnlockLevel = double.TryParse(maxUnlockLevel.Text, out var max) ? max : 100;

                viewModel.Filter();
                await PopupNavigation.Instance.PopAsync();
            }
        }
    }
}