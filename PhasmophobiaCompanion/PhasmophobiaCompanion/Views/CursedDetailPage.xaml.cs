using System;
using PhasmophobiaCompanion.Models;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CursedDetailPage : ContentPage
    {
        public CursedDetailPage(CursedPossession selectedCursed)
        {
            try
            {
                InitializeComponent();
                BindingContext = selectedCursed;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации CursedDetailPage.");
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
                Log.Error(ex, "Ошибка во время раскрытия или сворачивания списка на странице CursedDetailPage.");
                throw;
            }
        }
    }
}