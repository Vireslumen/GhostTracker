using System;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CursedsPage : ContentPage
    {
        public CursedsPage()
        {
            InitializeComponent();
            var viewModel = new CursedViewModel();
            BindingContext = viewModel;
        }

        /// <summary>
        ///     Переход на подробную страницу проклятого предмета по нажатию на него.
        /// </summary>
        private void OnCursedTapped(object sender, EventArgs e)
        {
            if (sender is View view && view.BindingContext is CursedPossession selectedCursed)
            {
                var detailPage = new CursedDetailPage(selectedCursed);
                Application.Current.MainPage.Navigation.PushAsync(detailPage);
            }
        }

        /// <summary>
        ///     Выполняет поиск после его завершения.
        /// </summary>
        private void OnSearchCompleted(object sender, EventArgs e)
        {
            if (BindingContext is CursedViewModel viewModel) viewModel.SearchCommand.Execute(null);
        }
    }
}