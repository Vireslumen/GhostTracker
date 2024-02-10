using System;
using PhasmophobiaCompanion.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClueDetailPage : ContentPage
    {
        public ClueDetailPage(Clue clue)
        {
            InitializeComponent();
            BindingContext = clue;
        }

        /// <summary>
        ///     Переход на страницу улики при нажатии на неё.
        /// </summary>
        private void OnClueTapped(object sender, EventArgs e)
        {
            var parentStack = sender as StackLayout;
            if (parentStack?.BindingContext is Clue clueItem)
            {
                var Page = new ClueDetailPage(clueItem);
                Application.Current.MainPage.Navigation.PushAsync(Page);
            }
        }

        /// <summary>
        ///     Переход на страницу призрака при нажатии на него.
        /// </summary>
        private void OnGhostTapped(object sender, EventArgs e)
        {
            var parentStack = sender as StackLayout;
            if (parentStack?.BindingContext is Ghost ghostItem)
            {
                var Page = new GhostDetailPage(ghostItem);
                Application.Current.MainPage.Navigation.PushAsync(Page);
            }
        }

        /// <summary>
        ///     Раскрытие или свертывание раскрывающегося элемента по нажатию на него.
        /// </summary>
        private void OnItemTapped(object sender, EventArgs e)
        {
            if (sender is StackLayout layout && layout.BindingContext is UnfoldingItem unfoldingItem)
                unfoldingItem.IsExpanded = !unfoldingItem.IsExpanded;
        }
    }
}