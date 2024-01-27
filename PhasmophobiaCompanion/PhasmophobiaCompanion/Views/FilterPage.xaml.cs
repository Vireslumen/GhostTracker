using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class FilterPage : PopupPage
    {
        private bool isSelectionHandlingEnabled = true;
        public FilterPage(GhostsViewModel viewModel)
        {
            InitializeComponent();
            cluesCollectionView.SelectionChanged += OnItemSelected;
            BindingContext = viewModel;
        }
        private void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            if (BindingContext is GhostsViewModel viewModel)
            {
                if (isSelectionHandlingEnabled)
                {

                    viewModel.SelectedClues.Clear();
                    foreach (Clue item in e.CurrentSelection)
                    {
                        viewModel.SelectedClues.Add(item);
                    }
                }
            }

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is GhostsViewModel viewModel)
            {
                // Отключите обработку выбора элементов
                isSelectionHandlingEnabled = false;

                // Очистите выбранные элементы
                cluesCollectionView.SelectedItems.Clear();

                // Выберите элементы в соответствии с SelectedClues
                foreach (var selectedClue in viewModel.SelectedClues)
                {

                    var clue = viewModel.AllClues.FirstOrDefault(c => c.Title == selectedClue.Title);
                    if (!string.IsNullOrEmpty(clue.Title))
                    {
                        cluesCollectionView.SelectedItems.Add(clue);
                    }
                }

                // Включите обработку выбора элементов
                isSelectionHandlingEnabled = true;
            }
        }
        private async void OnApplyFiltersClicked(object sender, EventArgs e)
        {
            if (BindingContext is GhostsViewModel viewModel)
            {

                viewModel.Filter();
                await PopupNavigation.Instance.PopAsync();
            }
        }

    }
}