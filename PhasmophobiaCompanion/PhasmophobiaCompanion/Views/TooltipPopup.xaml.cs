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
                Message = message;
                BindingContext = this;
            }
            catch (System.Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации TooltipPopup.");
                throw;
            }
        }

        public string Message { get; set; }

        /// <summary>
        ///     Закрытие всплывающей подсказки по нажатию на фон.
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackgroundClicked()
        {
            try
            {
                PopupNavigation.Instance.PopAsync();
                return false;
            }
            catch (System.Exception ex)
            {
                Log.Error(ex, "Ошибка во время закрытия всплывающей подсказки TooltipPopup.");
                throw;
            }
        }
    }
}