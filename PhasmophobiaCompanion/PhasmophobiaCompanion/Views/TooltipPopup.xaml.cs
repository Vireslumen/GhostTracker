using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TooltipPopup : PopupPage
    {
        public TooltipPopup(string message)
        {
            InitializeComponent();
            Message = message;
            BindingContext = this;
        }

        public string Message { get; set; }

        /// <summary>
        ///     Закрытие всплывающей подсказки по нажатию на фон.
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackgroundClicked()
        {
            PopupNavigation.Instance.PopAsync();
            return false;
        }
    }
}