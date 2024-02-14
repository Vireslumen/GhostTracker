using System;
using PhasmophobiaCompanion.Models;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClueDetailPage : ContentPage
    {
        public ClueDetailPage(Clue clue)
        {
            try
            {
                InitializeComponent();
                BindingContext = clue;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации ClueDetailPage.");
                throw;
            }
        }

        /// <summary>
        ///     Переход на страницу улики при нажатии на неё.
        /// </summary>
        private void OnClueTapped(object sender, EventArgs e)
        {
            try
            {
                var parentStack = sender as StackLayout;
                if (parentStack?.BindingContext is Clue clueItem)
                {
                    var Page = new ClueDetailPage(clueItem);
                    Application.Current.MainPage.Navigation.PushAsync(Page);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время перехода на страницу улики с подробной страницы улики.");
                throw;
            }
        }

        /// <summary>
        ///     Переход на страницу призрака при нажатии на него.
        /// </summary>
        private void OnGhostTapped(object sender, EventArgs e)
        {
            try
            {
                var parentStack = sender as StackLayout;
                if (parentStack?.BindingContext is Ghost ghostItem)
                {
                    var Page = new GhostDetailPage(ghostItem);
                    Application.Current.MainPage.Navigation.PushAsync(Page);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время перехода на страницу призрака с подробной страницы улики.");
                throw;
            }
        }

        /// <summary>
        ///     Раскрытие или свертывание раскрывающегося элемента по нажатию на него.
        /// </summary>
        private void OnItemTapped(object sender, EventArgs e)
        {
            try
            {
                if (sender is StackLayout layout && layout.BindingContext is UnfoldingItem unfoldingItem)
                    unfoldingItem.IsExpanded = !unfoldingItem.IsExpanded;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время раскрытия или сворачивания списка на странице ClueDetailPage.");
                throw;
            }
        }
    }
}