using System;
using PhasmophobiaCompanion.Models;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GhostDetailPage : ContentPage
    {
        public GhostDetailPage(Ghost selectedGhost)
        {
            try
            {
                InitializeComponent();
                BindingContext = selectedGhost;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации GhostDetailPage.");
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
                Log.Error(ex, "Ошибка во время раскрытия или сворачивания списка на странице GhostDetailPage");
                throw;
            }
        }
    }
}