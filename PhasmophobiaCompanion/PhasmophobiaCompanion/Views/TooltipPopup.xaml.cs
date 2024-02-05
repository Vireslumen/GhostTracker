using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TooltipPopup : PopupPage
    {
        public string Message { get; set; }
        public TooltipPopup(string message)
        {
            InitializeComponent();
            Message = message;
            BindingContext = this;
        }
        protected override bool OnBackgroundClicked()
        {
            PopupNavigation.Instance.PopAsync();
            return false;
        }
    }
}