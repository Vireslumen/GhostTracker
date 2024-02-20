using System;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Serilog;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TooltipPopup : PopupPage
    {
        public TooltipPopup(string message)
        {
            try
            {
                InitializeComponent();
                BindingContext = message;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации TooltipPopup.");
                throw;
            }
        }

        /// <summary>
        ///     Закрытие всплывающей подсказки по нажатию на фон.
        /// </summary>
        protected override bool OnBackgroundClicked()
        {
            try
            {
                PopupNavigation.Instance.PopAsync();
                return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время закрытия всплывающей подсказки TooltipPopup.");
                throw;
            }
        }
    }
}